using UnityEngine;

/// <summary>
/// Una oveja que avanza con el mundo. Si el jugador lleva un dragón, la
/// quema automáticamente: apunta su llama durante unos segundos y la oveja
/// explota como palomita dando monedas. El Jinete no puede quemarla.
/// </summary>
public class OvejaEnJuego : MonoBehaviour
{
    private const float SegundosDeQuema = 2f;
    private const float Alcance = 6f;

    private Oveja oveja;
    private DragonController dragon;
    private Transform llama;
    private bool quemando;
    private float tiempoQuemando;

    /// <summary>Lo llama la fábrica al crear la oveja.</summary>
    public void Inicializar(Oveja ovejaDominio)
    {
        oveja = ovejaDominio;
    }

    private void Start()
    {
        dragon = FindFirstObjectByType<DragonController>();
    }

    private void Update()
    {
        // Se mueve con la misma velocidad que las columnas.
        float factor = GameManager.Instancia != null ? GameManager.Instancia.MultiplicadorVelocidad : 1f;
        transform.position += Vector3.left * ModoJuego.VelocidadBase * factor * Time.deltaTime;

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
            return;
        }

        if (dragon == null || !Oveja.PuedeQuemarla(dragon.Personaje)) return;

        float distancia = transform.position.x - dragon.transform.position.x;

        if (!quemando && distancia > 0f && distancia < Alcance)
        {
            quemando = true;
            tiempoQuemando = 0f;
            CrearLlama();
        }

        if (quemando)
        {
            tiempoQuemando += Time.deltaTime;
            ActualizarLlama();

            if (tiempoQuemando >= SegundosDeQuema)
            {
                Explotar();
            }
        }
    }

    private void CrearLlama()
    {
        GameObject objeto = new GameObject("Llama");
        llama = objeto.transform;

        SpriteRenderer render = objeto.AddComponent<SpriteRenderer>();
        render.sprite = ArteJuego.Placeholder(FormaPlaceholder.Cuadrado);
        render.color = new Color(1f, 0.55f, 0.1f, 0.9f);
        render.sortingOrder = 4;
    }

    private void ActualizarLlama()
    {
        if (llama == null) return;

        Vector3 desde = dragon.transform.position;
        Vector3 hacia = transform.position;
        Vector3 direccion = hacia - desde;

        llama.position = (desde + hacia) / 2f;
        llama.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg);
        llama.localScale = new Vector3(direccion.magnitude, 0.18f, 1f);
    }

    private void Explotar()
    {
        PalomitasEfecto.Crear(transform.position);
        GameManager.Instancia.RecogerObjeto(oveja);
        GameManager.Instancia.NotificarMensaje("¡La oveja explotó! +" + oveja.Recompensa + " monedas");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (llama != null)
        {
            Destroy(llama.gameObject);
        }
    }
}
