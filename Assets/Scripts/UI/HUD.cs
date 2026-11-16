using TMPro;
using UnityEngine;

/// <summary>
/// Muestra puntos, Game Over y el estado de la habilidad del personaje.
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private GameObject textoGameOver;
    [SerializeField] private TextMeshProUGUI textoHabilidad;

    private DragonController dragon;

    private void Start()
    {
        GameManager.Instancia.OnPuntosCambiados += ActualizarPuntos;
        GameManager.Instancia.OnGameOver += MostrarGameOver;

        dragon = FindFirstObjectByType<DragonController>();

        ActualizarPuntos(0);
        textoGameOver.SetActive(false);
    }

    private void Update()
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

    private void OnDestroy()
    {
        if (GameManager.Instancia == null) return;
        GameManager.Instancia.OnPuntosCambiados -= ActualizarPuntos;
        GameManager.Instancia.OnGameOver -= MostrarGameOver;
    }

    private void ActualizarPuntos(int puntos)
    {
        textoPuntos.text = puntos.ToString();
    }

    private void MostrarGameOver()
    {
        textoGameOver.SetActive(true);
    }
}