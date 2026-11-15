using UnityEngine;

/// <summary>
/// Controla el movimiento vertical del dragón: cada vez que el jugador
/// pulsa espacio o hace clic, el dragón da un aleteo hacia arriba.
/// Si choca con una torre o se sale de la pantalla, avisa al GameManager.
/// </summary>
public class DragonController : MonoBehaviour
{
    /// <summary>Velocidad hacia arriba que recibe el dragón en cada aleteo.</summary>
    [SerializeField] private float fuerzaAleteo = 5f;

    /// <summary>Altura (Y) por debajo de la cual el dragón pierde.</summary>
    [SerializeField] private float limiteInferior = -5.5f;

    /// <summary>Altura (Y) por encima de la cual el dragón pierde.</summary>
    [SerializeField] private float limiteSuperior = 5.5f;

    /// <summary>Componente de física 2D del dragón.</summary>
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Con la partida terminada, el dragón ya no responde.
        if (GameManager.Instancia.Estado != EstadoPartida.Jugando) return;

        bool aleteo = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (aleteo)
        {
            rb.linearVelocity = Vector2.up * fuerzaAleteo;
        }

        // Si se sale por arriba o por abajo de la pantalla, pierde.
        if (transform.position.y < limiteInferior || transform.position.y > limiteSuperior)
        {
            Debug.Log("¡Game Over! El dragón salió de la pantalla");
            GameManager.Instancia.TerminarPartida();
        }
    }

    /// <summary>Se llama al chocar con otro collider (las torres).</summary>
    private void OnCollisionEnter2D(Collision2D colision)
    {
        Debug.Log("¡Game Over! Chocaste con " + colision.collider.name);
        GameManager.Instancia.TerminarPartida();
    }
}