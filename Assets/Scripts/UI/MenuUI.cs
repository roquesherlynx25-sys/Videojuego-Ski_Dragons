using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el menú principal: elegir modo y personaje, ver los objetos
/// equipables, iniciar la partida y salir de la aplicación.
/// </summary>
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoModo;
    [SerializeField] private TextMeshProUGUI textoPersonaje;
    [SerializeField] private TextMeshProUGUI textoDescripcion;
    [SerializeField] private GameObject panelEquipables;
    [SerializeField] private TextMeshProUGUI textoSesion; // opcional
    [SerializeField] private string nombreEscenaJuego = "Juego";

    /// <summary>Personaje que se va a llevar a la partida.</summary>
    private Personaje personajeElegido;

    private void Start()
    {
        panelEquipables.SetActive(false);
        ElegirModoInfinito();
        ElegirJinete();
    }

    private void Update()
    {
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

    // ---------- Modos (un método por botón) ----------
    public void ElegirModoInfinito() { ElegirModo(TipoModo.Infinito); }
    public void ElegirModoNiveles() { ElegirModo(TipoModo.Niveles); }
    public void ElegirModoBiomas() { ElegirModo(TipoModo.Biomas); }

    private void ElegirModo(TipoModo modo)
    {
        SesionJuego.ModoSeleccionado = modo;
        textoModo.text = "Modo: " + modo;
        textoDescripcion.text = DescribirModo(modo);
    }

    private string DescribirModo(TipoModo modo)
    {
        switch (modo)
        {
            case TipoModo.Infinito:
                return "Sin final: suma puntos por cada torre y monedas.";
            case TipoModo.Niveles:
                return "Capas de la atmósfera con porcentaje de avance. (En construcción)";
            default:
                return "Objetos, enemigos y desbloqueo con monedas. (En construcción)";
        }
    }

    // ---------- Personajes ----------
    public void ElegirJinete()
    {
        personajeElegido = new Jinete(SesionJuego.NombreUsuario);
        MostrarPersonaje("Sin habilidad.");
    }

    public void ElegirAndary()
    {
        personajeElegido = new DragonAndary();
        MostrarPersonaje("Habilidad Tiempo: reduce el tiempo a la mitad.");
    }

    public void ElegirGorgon()
    {
        personajeElegido = new DragonGorgon();
        MostrarPersonaje("Habilidad Dureza: escudo temporal contra obstáculos.");
    }

    private void MostrarPersonaje(string descripcion)
    {
        textoPersonaje.text = "Personaje: " + personajeElegido.Nombre;
        textoDescripcion.text = personajeElegido.Rol + ". " + descripcion;
    }

    // ---------- Equipables ----------
    public void AbrirEquipables() { panelEquipables.SetActive(true); }
    public void CerrarEquipables() { panelEquipables.SetActive(false); }

    // ---------- Iniciar y salir ----------
    public void Iniciar()
    {
        if (SesionJuego.ModoSeleccionado != TipoModo.Infinito)
        {
            textoDescripcion.text = "Ese modo aún está en construcción. Elige Infinito.";
            return;
        }

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
