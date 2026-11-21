using TMPro;
using UnityEngine;

/// <summary>
/// Muestra en pantalla el progreso del modo (puntos o nivel y etapa, y
/// monedas), el estado de la habilidad, los fragmentos del Ángel y los
/// mensajes de Game Over y Victoria. Escucha los eventos del GameManager.
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private GameObject textoGameOver;
    [SerializeField] private TextMeshProUGUI textoHabilidad;
    [SerializeField] private TextMeshProUGUI textoAngel; // opcional

    private DragonController dragon;

    private void Start()
    {
        GameManager gm = GameManager.Instancia;
        gm.OnProgresoCambiado += ActualizarProgreso;
        gm.OnGameOver += MostrarGameOver;
        gm.OnVictoria += MostrarVictoria;

        dragon = FindFirstObjectByType<DragonController>();

        ActualizarProgreso();
        textoGameOver.SetActive(false);
    }

    private void Update()
    {
        ActualizarHabilidad();
        ActualizarAngel();
    }

    private void OnDestroy()
    {
        if (GameManager.Instancia == null) return;

        GameManager.Instancia.OnProgresoCambiado -= ActualizarProgreso;
        GameManager.Instancia.OnGameOver -= MostrarGameOver;
        GameManager.Instancia.OnVictoria -= MostrarVictoria;
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
            Habilidad h = d.Habilidad;
            string estado = h.EstaActiva ? "ACTIVA"
                          : h.EstaLista ? "LISTA (pulsa E)"
                          : "recarga " + h.RecargaRestante.ToString("0.0") + " s";
            textoHabilidad.text = h.Nombre + ": " + estado;
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
        Angel angel = jugador.ObtenerEquipado<Angel>();

        textoAngel.text = angel == null
            ? ""
            : "Ángel: " + jugador.FragmentosAngel + "/" + Angel.FragmentosNecesarios + " fragmentos";
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

        MostrarMensaje("GAME OVER\n" + gm.Modo.TextoProgreso() + "\n" + record + "Pulsa R para reiniciar");
    }

    private void MostrarVictoria()
    {
        GameManager gm = GameManager.Instancia;
        MostrarMensaje("¡VICTORIA!\n" + gm.Modo.TextoProgreso() + "\nPulsa R para reiniciar");
    }

    private void MostrarMensaje(string mensaje)
    {
        textoGameOver.SetActive(true);

        TextMeshProUGUI texto = textoGameOver.GetComponent<TextMeshProUGUI>();
        if (texto != null)
        {
            texto.text = mensaje;
        }
    }
}
