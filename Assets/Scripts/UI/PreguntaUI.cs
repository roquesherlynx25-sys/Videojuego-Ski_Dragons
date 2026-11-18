using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel con la pregunta de POO que aparece cuando el jugador tiene 2
/// fragmentos del Ángel y pierde. Muestra la pregunta y sus opciones y
/// avisa al GameManager cuál se eligió.
/// IMPORTANTE: este script va en el Canvas (no en el panel), porque el
/// panel se oculta y el script debe seguir activo.
/// </summary>
public class PreguntaUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textoPregunta;
    [SerializeField] private Button[] botonesOpciones;

    private void Start()
    {
        panel.SetActive(false);

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int indice = i; // copia local: cada botón recuerda su propio número
            botonesOpciones[i].onClick.AddListener(() => Responder(indice));
        }

        GameManager.Instancia.OnPreguntaRevivir += Mostrar;
    }

    private void OnDestroy()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.OnPreguntaRevivir -= Mostrar;
        }
    }

    /// <summary>Muestra la pregunta y reparte sus opciones en los botones.</summary>
    private void Mostrar(PreguntaPOO pregunta)
    {
        textoPregunta.text = pregunta.Texto;

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            bool existe = i < pregunta.CantidadOpciones;
            botonesOpciones[i].gameObject.SetActive(existe);

            if (existe)
            {
                TextMeshProUGUI texto = botonesOpciones[i].GetComponentInChildren<TextMeshProUGUI>();
                texto.text = pregunta.ObtenerOpcion(i);
            }
        }

        panel.SetActive(true);
    }

    private void Responder(int indice)
    {
        panel.SetActive(false);
        GameManager.Instancia.ResponderPregunta(indice);
    }
}
