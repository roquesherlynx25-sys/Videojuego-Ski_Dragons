using UnityEngine;

/// <summary>
/// Cambia poco a poco el color del cielo (fondo de la cámara) según el
/// modo, el nivel o el bioma. Si existe una imagen en
/// Resources/Sprites/fondo_NOMBRE.png, la dibuja detrás de todo.
/// Va en el objeto Main Camera.
/// </summary>
[RequireComponent(typeof(Camera))]
public class FondoNivel : MonoBehaviour
{
    /// <summary>Qué tan rápido se mezcla un color con el siguiente.</summary>
    [SerializeField] private float velocidadCambio = 2f;

    private Camera camara;
    private SpriteRenderer fondoSprite;
    private string fondoActual = "";

    private void Awake()
    {
        camara = GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameManager.Instancia == null) return;

        ModoJuego modo = GameManager.Instancia.Modo;

        ColorRgb color = modo.ColorFondo;
        Color objetivo = new Color(color.R, color.G, color.B);
        camara.backgroundColor = Color.Lerp(camara.backgroundColor, objetivo, Time.unscaledDeltaTime * velocidadCambio);

        if (modo.NombreFondo != fondoActual)
        {
            fondoActual = modo.NombreFondo;
            ActualizarImagen(fondoActual);
        }
    }

    private void ActualizarImagen(string nombre)
    {
        Sprite imagen = ArteJuego.Cargar("fondo_" + nombre);

        if (imagen == null)
        {
            if (fondoSprite != null) fondoSprite.enabled = false;
            return;
        }

        if (fondoSprite == null)
        {
            GameObject objeto = new GameObject("FondoImagen");
            objeto.transform.SetParent(transform, false);
            objeto.transform.localPosition = new Vector3(0f, 0f, 10f);
            fondoSprite = objeto.AddComponent<SpriteRenderer>();
            fondoSprite.sortingOrder = -100;
        }

        fondoSprite.sprite = imagen;
        fondoSprite.enabled = true;

        // Se escala para cubrir toda la pantalla.
        float alto = camara.orthographicSize * 2f;
        float ancho = alto * camara.aspect;
        float escala = Mathf.Max(ancho / imagen.bounds.size.x, alto / imagen.bounds.size.y);
        fondoSprite.transform.localScale = new Vector3(escala, escala, 1f);
    }
}
