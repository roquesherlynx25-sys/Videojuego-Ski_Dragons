using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla la partida: estado, modo de juego, puntos, monedas, revivir
/// con el Ángel, victoria, Game Over y reinicio. Es un singleton: solo
/// existe uno y se accede con GameManager.Instancia.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>Única instancia del GameManager en la escena.</summary>
    public static GameManager Instancia { get; private set; }

    /// <summary>Estado actual de la partida.</summary>
    public EstadoPartida Estado { get; private set; } = EstadoPartida.Jugando;

    /// <summary>Modo de juego en curso (Infinito, Niveles o Biomas).</summary>
    public ModoJuego Modo { get; private set; }

    /// <summary>Puntos de la partida (obstáculos superados).</summary>
    public int Puntos => Modo.Puntos;

    /// <summary>Velocidad de los obstáculos según el modo y el nivel (1 = normal).</summary>
    public float MultiplicadorVelocidad => Modo.MultiplicadorVelocidad;

    /// <summary>Mejor puntuación guardada del modo Infinito.</summary>
    public int Record { get; private set; }

    /// <summary>True si la partida que acaba de terminar superó el récord.</summary>
    public bool NuevoRecord { get; private set; }

    /// <summary>Evento: cambiaron los puntos, las monedas o el nivel.</summary>
    public event Action OnProgresoCambiado;

    /// <summary>Evento: la partida terminó por derrota.</summary>
    public event Action OnGameOver;

    /// <summary>Evento: el jugador completó el modo finito.</summary>
    public event Action OnVictoria;

    /// <summary>Evento: hay que mostrar una pregunta de POO para revivir.</summary>
    public event Action<PreguntaPOO> OnPreguntaRevivir;

    /// <summary>Evento: el jugador revivió (el dragón debe reaparecer).</summary>
    public event Action OnRevivir;

    /// <summary>Evento: hay un mensaje corto para mostrar en pantalla.</summary>
    public event Action<string> OnMensaje;

    /// <summary>Nombre de la escena del menú (se abre con la tecla M al terminar).</summary>
    private const string EscenaMenu = "MenuPrincipal";

    // El Ángel es el mismo para todos: con 3 fragmentos (o 2 y una pregunta) se revive.
    private readonly Angel angel = new Angel();
    private PreguntaPOO preguntaActual;

    private void Awake()
    {
        // Si ya existe otra instancia, esta sobra y se elimina.
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        Time.timeScale = 1f; // Por si venimos de un Game Over (tiempo congelado)

        Modo = CrearModo(SesionJuego.ModoSeleccionado);
        Modo.OnProgresoCambiado += AvisarProgreso;
        Record = Persistencia.CargarRecordInfinito();

        // Reinicia los fragmentos, equipa las runas y gasta el Ángel de reserva.
        SesionJuego.Jugador.PrepararPartida();
        Persistencia.Guardar(SesionJuego.Jugador);
    }

    private void Update()
    {
        // Al terminar la partida (derrota o victoria): R reinicia y M vuelve al menú.
        bool terminada = Estado == EstadoPartida.GameOver || Estado == EstadoPartida.Victoria;
        if (!terminada) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reiniciar();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(EscenaMenu);
        }
    }

    /// <summary>Crea el modo elegido en el menú.</summary>
    private static ModoJuego CrearModo(TipoModo tipo)
    {
        switch (tipo)
        {
            case TipoModo.Niveles:
                return new ModoNiveles();
            case TipoModo.Biomas:
                return new ModoBiomas(SesionJuego.BiomaSeleccionado);
            case TipoModo.Infinito:
            default:
                return new ModoInfinito();
        }
    }

    private void AvisarProgreso()
    {
        OnProgresoCambiado?.Invoke();
    }

    /// <summary>Muestra un mensaje corto en pantalla.</summary>
    public void NotificarMensaje(string mensaje)
    {
        OnMensaje?.Invoke(mensaje);
    }

    /// <summary>Suma un obstáculo superado, solo si la partida sigue en curso.</summary>
    public void SumarPunto()
    {
        if (Estado != EstadoPartida.Jugando) return;

        Modo.RegistrarObstaculo();
        ComprobarVictoria();
    }

    /// <summary>Si el modo ya terminó (nivel final completado), gana la partida.</summary>
    public void ComprobarVictoria()
    {
        if (Estado == EstadoPartida.Jugando && Modo.HaTerminado)
        {
            Ganar();
        }
    }

    /// <summary>El dragón recogió un objeto (moneda, oveja...): se suma al jugador y al modo.</summary>
    public void RecogerObjeto(ObjetoJuego objeto)
    {
        if (Estado != EstadoPartida.Jugando || objeto == null) return;

        EstadoJugador jugador = SesionJuego.Jugador;
        int antes = jugador.Monedas;
        objeto.AlRecoger(jugador);
        Modo.RegistrarMoneda(jugador.Monedas - antes); // incluye el multiplicador
    }

    /// <summary>El dragón recogió una moneda.</summary>
    public void RecogerMoneda(Moneda moneda)
    {
        RecogerObjeto(moneda);
    }

    /// <summary>El dragón recogió un fragmento de Ángel.</summary>
    public void RecogerFragmentoAngel()
    {
        if (Estado != EstadoPartida.Jugando) return;

        angel.AlRecoger(SesionJuego.Jugador);
        NotificarMensaje("¡Fragmento de Ángel! " + SesionJuego.Jugador.FragmentosAngel + "/" + Angel.FragmentosNecesarios);
    }

    /// <summary>
    /// El dragón perdió (choque o salió de pantalla). Con 3 fragmentos del
    /// Ángel revive; con 2, responde una pregunta; si no, la partida termina.
    /// </summary>
    public void TerminarPartida()
    {
        if (Estado != EstadoPartida.Jugando) return;

        EstadoJugador jugador = SesionJuego.Jugador;

        if (angel.PuedeRevivir(jugador))
        {
            Revivir();
            return;
        }

        if (angel.PuedeRevivirConPregunta(jugador))
        {
            IniciarPregunta();
            return;
        }

        FinalizarPartida();
    }

    /// <summary>La pregunta de POO fue respondida: se elige la opción indicada.</summary>
    public void ResponderPregunta(int indiceElegido)
    {
        if (Estado != EstadoPartida.Pregunta || preguntaActual == null) return;

        bool correcta = preguntaActual.EsCorrecta(indiceElegido);
        preguntaActual = null;

        if (correcta)
        {
            SesionJuego.Jugador.AgregarFragmentoAngel(); // el fragmento que faltaba
            Revivir();
        }
        else
        {
            FinalizarPartida();
        }
    }

    private void IniciarPregunta()
    {
        preguntaActual = BancoPreguntas.Instancia.ObtenerAleatoria();
        Estado = EstadoPartida.Pregunta;
        Time.timeScale = 0f;
        OnPreguntaRevivir?.Invoke(preguntaActual);
    }

    private void Revivir()
    {
        angel.Consumir(SesionJuego.Jugador);
        Persistencia.Guardar(SesionJuego.Jugador);

        // El Ángel limpia los enemigos que hay en pantalla.
        EnemigoEnJuego[] enemigos = FindObjectsByType<EnemigoEnJuego>(FindObjectsSortMode.None);
        foreach (EnemigoEnJuego enemigo in enemigos)
        {
            Destroy(enemigo.gameObject);
        }

        Estado = EstadoPartida.Jugando;
        Time.timeScale = 1f;
        OnRevivir?.Invoke();
        NotificarMensaje("¡El Ángel te revivió!");
        AvisarProgreso();
    }

    private void Ganar()
    {
        Estado = EstadoPartida.Victoria;
        Time.timeScale = 0f;

        Persistencia.Guardar(SesionJuego.Jugador);
        OnVictoria?.Invoke();
    }

    private void FinalizarPartida()
    {
        Estado = EstadoPartida.GameOver;
        Time.timeScale = 0f;

        if (Modo is ModoInfinito)
        {
            NuevoRecord = Persistencia.GuardarRecordInfinito(Puntos);
            if (NuevoRecord) Record = Puntos;
        }

        Persistencia.Guardar(SesionJuego.Jugador);
        OnGameOver?.Invoke();
    }

    /// <summary>Recarga la escena actual para empezar de cero.</summary>
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
