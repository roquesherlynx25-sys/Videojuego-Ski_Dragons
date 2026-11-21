using UnityEngine;

/// <summary>
/// Cambia poco a poco el color del cielo (fondo de la cámara) según el
/// nivel del modo Niveles. En los demás modos no hace nada.
/// Va en el objeto Main Camera.
/// </summary>
[RequireComponent(typeof(Camera))]
public class FondoNivel : MonoBehaviour
{
    /// <summary>Qué tan rápido se mezcla un color con el siguiente.</summary>
    [SerializeField] private float velocidadCambio = 2f;

    private Camera camara;

    private void Awake()
    {
        camara = GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameManager.Instancia == null) return;

        if (GameManager.Instancia.Modo is ModoNiveles modo)
        {
            ColorRgb color = modo.NivelActual.ColorFondo;
            Color objetivo = new Color(color.R, color.G, color.B);

            camara.backgroundColor = Color.Lerp(
                camara.backgroundColor, objetivo, Time.unscaledDeltaTime * velocidadCambio);
        }
    }
}
