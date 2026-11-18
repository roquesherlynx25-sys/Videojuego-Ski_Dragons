using TMPro;
using UnityEngine;

/// <summary>
/// Muestra en pantalla el progreso del modo (puntos y monedas), el estado
/// de la habilidad, los fragmentos del Ángel y el mensaje de Game Over.
/// Escucha los eventos del GameManager.
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
        textoGameOver.SetActive(true);

        TextMeshProUGUI texto = textoGameOver.GetComponent<TextMeshProUGUI>();
        if (texto == null) return;

        GameManager gm = GameManager.Instancia;
        string record = gm.NuevoRecord ? "¡NUEVO RÉCORD!" : "Récord: " + gm.Record;

        texto.text = "GAME OVER\n" + gm.Modo.TextoProgreso() + "\n" + record + "\nPulsa R para reiniciar";
    }
}
