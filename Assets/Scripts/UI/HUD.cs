using TMPro;
using UnityEngine;

///Muestra en pantalla los puntos y el mensaje de Game Over.
///Escucha los eventos del GameManager.
public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private GameObject textoGameOver;

    private void Start()
    {
        GameManager.Instancia.OnPuntosCambiados += ActualizarPuntos;
        GameManager.Instancia.OnGameOver += MostrarGameOver;

        ActualizarPuntos(0);
        textoGameOver.SetActive(false);
    }

    private void OnDestroy()
    {
        //Nos damos de baja de los eventos para evitar errores al reiniciar.
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