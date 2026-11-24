using TMPro;
using UnityEngine;

/// <summary>
/// Muestra en pantalla el progreso del modo (puntos, nivel o bioma, y
/// monedas), el estado de la habilidad, los fragmentos del Ángel, la Runa
/// de Escudo, mensajes cortos y los finales (Game Over y Victoria).
/// Escucha los eventos del GameManager. Va en el Canvas.
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private GameObject textoGameOver;
    [SerializeField] private TextMeshProUGUI textoHabilidad;
    [SerializeField] private TextMeshProUGUI textoAngel; // opcional

    private DragonController dragon;
    private TextMeshProUGUI textoMensaje;
    private float mensajeHasta;

    private void Start()
    {
        GameManager gm = GameManager.Instancia;
        gm.OnProgresoCambiado += ActualizarProgreso;
        gm.OnGameOver += MostrarGameOver;
        gm.OnVictoria += MostrarVictoria;
        gm.OnMensaje += MostrarMensaje;

        dragon = FindFirstObjectByType<DragonController>();

        // El texto de los mensajes cortos se crea desde código (arriba, al centro).
        textoMensaje = UiRuntime.CrearTexto(transform, "TextoMensaje", "", 36f, Vector2.zero,
            new Vector2(1400f, 60f), TextAlignmentOptions.Center, new Color(1f, 0.92f, 0.4f));
        RectTransform rect = textoMensaje.rectTransform;
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0f, -270f);

        ActualizarProgreso();
        textoGameOver.SetActive(false);
    }

    private void Update()
    {
        ActualizarHabilidad();
        ActualizarAngel();

        if (textoMensaje != null && Time.unscaledTime > mensajeHasta && textoMensaje.text != "")
        {
            textoMensaje.text = "";
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instancia == null) return;

        GameManager.Instancia.OnProgresoCambiado -= ActualizarProgreso;
        GameManager.Instancia.OnGameOver -= MostrarGameOver;
        GameManager.Instancia.OnVictoria -= MostrarVictoria;
        GameManager.Instancia.OnMensaje -= MostrarMensaje;
    }

    private void ActualizarProgreso()
    {
        textoPuntos.text = GameManager.Instancia.Modo.TextoProgreso();
    }

    private void ActualizarHabilidad()
    {
        if (textoHabilidad == null || dragon == null || dragon.Personaje == null) return;

        if (dragon.Personaje is Dragon d)
        {
            textoHabilidad.text = d.Habilidad.Nombre + ": " + EstadoDe(d.Habilidad, "E");
        }
        else
        {
            textoHabilidad.text = "Sin habilidad";
        }
    }

    private void ActualizarAngel()
    {
        if (textoAngel == null) return;

        EstadoJugador jugador = SesionJuego.Jugador;
        string texto = "Ángel: " + jugador.FragmentosAngel + "/" + Angel.FragmentosNecesarios + " fragmentos";

        RunaEscudo runa = jugador.ObtenerEquipado<RunaEscudo>();
        if (runa != null)
        {
            texto += "     Runa de Escudo: " + EstadoDe(runa.Escudo, "Q");
        }

        textoAngel.text = texto;
    }

    /// <summary>Texto con el estado de una habilidad: activa, lista o recargando.</summary>
    private static string EstadoDe(Habilidad habilidad, string tecla)
    {
        if (habilidad.EstaActiva) return "ACTIVA";
        if (habilidad.EstaLista) return "LISTA (pulsa " + tecla + ")";
        return "recarga " + habilidad.RecargaRestante.ToString("0.0") + " s";
    }

    private void MostrarMensaje(string mensaje)
    {
        if (textoMensaje == null) return;

        textoMensaje.text = mensaje;
        mensajeHasta = Time.unscaledTime + 2.5f;
    }

    private void MostrarGameOver()
    {
        GameManager gm = GameManager.Instancia;

        // El récord solo existe en el modo Infinito.
        string record = "";
        if (gm.Modo is ModoInfinito)
        {
            record = (gm.NuevoRecord ? "¡NUEVO RÉCORD!" : "Récord: " + gm.Record) + "\n";
        }

        MostrarFinal("GAME OVER\n" + gm.Modo.TextoProgreso() + "\n" + record + "R: reiniciar   M: menú");
    }

    private void MostrarVictoria()
    {
        GameManager gm = GameManager.Instancia;
        MostrarFinal("¡VICTORIA!\n" + gm.Modo.TextoProgreso() + "\nR: reiniciar   M: menú");
    }

    private void MostrarFinal(string mensaje)
    {
        textoGameOver.SetActive(true);

        TextMeshProUGUI texto = textoGameOver.GetComponent<TextMeshProUGUI>();
        if (texto != null)
        {
            texto.text = mensaje;
        }
    }
}
