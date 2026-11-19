using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla la partida: estado, modo de juego, puntos, monedas, revivir
/// con el Ángel, Game Over y reinicio. Es un singleton: solo existe uno
/// y se accede con GameManager.Instancia.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>Única instancia del GameManager en la escena.</summary>
    public static GameManager Instancia { get; private set; }

    /// <summary>Estado actual de la partida.</summary>
    public EstadoPartida Estado { get; private set; } = EstadoPartida.Jugando;

    /// <summary>Modo de juego en curso (Infinito, por ahora).</summary>
    public ModoJuego Modo { get; private set; }

    /// <summary>Puntos de la partida (obstáculos superados).</summary>
    public int Puntos => Modo.Puntos;

    /// <summary>Mejor puntuación guardada del modo Infinito.</summary>
    public int Record { get; private set; }

    /// <summary>True si la partida que acaba de terminar superó el récord.</summary>
    public bool NuevoRecord { get; private set; }

    /// <summary>Evento: cambiaron los puntos o las monedas.</summary>
    public event Action OnProgresoCambiado;

    /// <summary>Evento: la partida terminó.</summary>
    public event Action OnGameOver;

    /// <summary>Evento: hay que mostrar una pregunta de POO para revivir.</summary>
    public event Action<PreguntaPOO> OnPreguntaRevivir;

    /// <summary>Evento: el jugador revivió (el dragón debe reaparecer).</summary>
    public event Action OnRevivir;

    private PreguntaPOO preguntaActual;
    private Angel angelEnUso;

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
    }

    private void Update()
    {
        // En Game Over, la tecla R reinicia la partida.
        if (Estado == EstadoPartida.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            Reiniciar();
        }
    }

    /// <summary>Crea el modo elegido. Por ahora solo existe el Infinito.</summary>
    private static ModoJuego CrearModo(TipoModo tipo)
    {
        switch (tipo)
        {
            case TipoModo.Infinito:
            default:
                return new ModoInfinito();
        }
    }

    private void AvisarProgreso()
    {
        OnProgresoCambiado?.Invoke();
    }

    /// <summary>Suma un obstáculo superado, solo si la partida sigue en curso.</summary>
    public void SumarPunto()
    {
        if (Estado != EstadoPartida.Jugando) return;

        Modo.RegistrarObstaculo();
    }

    /// <summary>El dragón recogió una moneda: se suma al jugador y al modo.</summary>
    public void RecogerMoneda(Moneda moneda)
    {
        if (Estado != EstadoPartida.Jugando || moneda == null) return;

        EstadoJugador jugador = SesionJuego.Jugador;
        int antes = jugador.Monedas;
        moneda.AlRecoger(jugador);
        Modo.RegistrarMoneda(jugador.Monedas - antes); // incluye el multiplicador
    }

    /// <summary>
    /// El dragón perdió (choque o salió de pantalla). Si tiene el Ángel
    /// equipado puede revivir; si no, la partida termina.
    /// </summary>
    public void TerminarPartida()
    {
        if (Estado != EstadoPartida.Jugando) return;

        EstadoJugador jugador = SesionJuego.Jugador;
        Angel angel = jugador.ObtenerEquipado<Angel>();

        if (angel != null && angel.PuedeRevivir(jugador))
        {
            Revivir(angel);
            return;
        }

        if (angel != null && angel.PuedeRevivirConPregunta(jugador))
        {
            IniciarPregunta(angel);
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
            Revivir(angelEnUso);
        }
        else
        {
            FinalizarPartida();
        }
    }

    private void IniciarPregunta(Angel angel)
    {
        angelEnUso = angel;
        preguntaActual = BancoPreguntas.Instancia.ObtenerAleatoria();
        Estado = EstadoPartida.Pregunta;
        Time.timeScale = 0f;
        OnPreguntaRevivir?.Invoke(preguntaActual);
    }

    private void Revivir(Angel angel)
    {
        angel.Consumir(SesionJuego.Jugador);
        Persistencia.Guardar(SesionJuego.Jugador);

        Estado = EstadoPartida.Jugando;
        Time.timeScale = 1f;
        OnRevivir?.Invoke();
        AvisarProgreso();
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
