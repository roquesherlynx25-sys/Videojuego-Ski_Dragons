using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el menú principal: elegir modo, personaje y bioma, abrir la
/// tienda y los equipables, iniciar la partida y salir. La tienda, los
/// equipables y los biomas se arman por código (no hay que dibujarlos).
/// </summary>
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoModo;
    [SerializeField] private TextMeshProUGUI textoPersonaje;
    [SerializeField] private TextMeshProUGUI textoDescripcion;
    [SerializeField] private GameObject panelEquipables; // panel antiguo: ya no se usa
    [SerializeField] private TextMeshProUGUI textoSesion; // opcional
    [SerializeField] private string nombreEscenaJuego = "Juego";

    /// <summary>Personaje que se va a llevar a la partida.</summary>
    private Personaje personajeElegido;

    private TiendaPanelUI tienda;
    private EquipablesPanelUI equipables;
    private BiomasPanelUI biomas;
    private TextMeshProUGUI textoMonedas;

    private void Start()
    {
        if (panelEquipables != null) panelEquipables.SetActive(false);

        tienda = new TiendaPanelUI(transform, ActualizarMonedas);
        equipables = new EquipablesPanelUI(transform, ActualizarMonedas);
        biomas = new BiomasPanelUI(transform, AlElegirBioma);

        // Texto de monedas (arriba a la derecha) y botón de la tienda, creados por código.
        textoMonedas = UiRuntime.CrearTexto(transform, "TextoMonedas", "", 36f, new Vector2(680f, 490f),
            new Vector2(500f, 60f), TextAlignmentOptions.MidlineRight, new Color(1f, 0.85f, 0.2f));
        UiRuntime.CrearBoton(transform, "BtnTienda", "Tienda", new Vector2(-700f, -350f),
            new Vector2(300f, 100f), AbrirTienda);

        ElegirModoInfinito();
        ElegirJinete();
        ActualizarMonedas();
    }

    private void Update()
    {
        ActualizarMonedas();

        // Muestra el usuario, la hora de entrada y el tiempo de permanencia.
        if (textoSesion == null) return;

        SessionManager sm = SessionManager.Instancia;
        if (sm != null && sm.Sesion != null)
        {
            textoSesion.text = "Usuario: " + sm.Sesion.Usuario +
                               "   Entrada: " + sm.Sesion.HoraEntrada.ToString("HH:mm:ss") +
                               "   Permanencia: " + sm.Sesion.PermanenciaTexto;
        }
        else
        {
            textoSesion.text = "Sin sesión (abre el juego desde Login)";
        }
    }

    private void ActualizarMonedas()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = "Monedas: " + SesionJuego.Jugador.Monedas;
        }
    }

    // ---------- Modos (un método por botón) ----------
    public void ElegirModoInfinito() { ElegirModo(TipoModo.Infinito); }
    public void ElegirModoNiveles() { ElegirModo(TipoModo.Niveles); }

    public void ElegirModoBiomas()
    {
        ElegirModo(TipoModo.Biomas);
        biomas.Mostrar();
    }

    private void ElegirModo(TipoModo modo)
    {
        SesionJuego.ModoSeleccionado = modo;

        if (modo == TipoModo.Biomas)
        {
            AlElegirBioma(SesionJuego.BiomaSeleccionado);
            return;
        }

        textoModo.text = "Modo: " + modo;
        textoDescripcion.text = DescribirModo(modo);
    }

    private string DescribirModo(TipoModo modo)
    {
        switch (modo)
        {
            case TipoModo.Infinito:
                return "Sin final: suma puntos por cada torre y monedas. Cada vez más difícil.";
            case TipoModo.Niveles:
                return "7 capas de la atmósfera, de la Tierra a la Galaxia, con el porcentaje de cada etapa.";
            default:
                return "Elige un bioma: enemigos, ovejas y dificultad propia.";
        }
    }

    /// <summary>Se llama al elegir un bioma en el panel de biomas.</summary>
    private void AlElegirBioma(Bioma bioma)
    {
        textoModo.text = "Modo: Biomas - " + bioma.Nombre;
        textoDescripcion.text = "Bioma " + bioma.Nombre + ": enemigos y ovejas (" + bioma.RecompensaOveja + " monedas cada una).";
    }

    // ---------- Personajes ----------
    public void ElegirJinete()
    {
        personajeElegido = new Jinete(SesionJuego.NombreUsuario);
        MostrarPersonaje("Sin habilidad.");
    }

    public void ElegirAndary()
    {
        EstadoJugador jugador = SesionJuego.Jugador;

        if (!jugador.Inventario.TienePersonaje(Inventario.NombreAndary))
        {
            textoDescripcion.text = "El Dragón Andary cuesta 350 monedas. Cómpralo en la Tienda.";
            return;
        }

        int nivel = jugador.Inventario.NivelHabilidad(Inventario.ClaveTiempo);
        personajeElegido = new DragonAndary(nivel);
        MostrarPersonaje("Habilidad Tiempo (nivel " + nivel + "): reduce el tiempo a la mitad con la tecla E.");
    }

    public void ElegirGorgon()
    {
        EstadoJugador jugador = SesionJuego.Jugador;

        if (!jugador.Inventario.TienePersonaje(Inventario.NombreGorgon))
        {
            textoDescripcion.text = "El Dragón Gorgón cuesta 800 monedas. Cómpralo en la Tienda.";
            return;
        }

        int nivel = jugador.Inventario.NivelHabilidad(Inventario.ClaveDureza);
        personajeElegido = new DragonGorgon(nivel);
        MostrarPersonaje("Dureza (nivel " + nivel + "): escudo con la tecla E y " + personajeElegido.Vidas + " vidas.");
    }

    private void MostrarPersonaje(string descripcion)
    {
        textoPersonaje.text = "Personaje: " + personajeElegido.Nombre;
        textoDescripcion.text = personajeElegido.Rol + ". " + descripcion;
    }

    // ---------- Tienda y equipables ----------
    public void AbrirTienda() { tienda.Mostrar(); }
    public void AbrirEquipables() { equipables.Mostrar(); }
    public void CerrarEquipables() { equipables.Ocultar(); }

    // ---------- Iniciar y salir ----------
    public void Iniciar()
    {
        if (personajeElegido == null) ElegirJinete();

        SesionJuego.Jugador.PersonajeSeleccionado = personajeElegido;
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    /// <summary>Cierra la sesión (escribe la bitácora) y sale de la aplicación.</summary>
    public void Salir()
    {
        if (SessionManager.Instancia != null)
        {
            SessionManager.Instancia.CerrarSesion();
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
