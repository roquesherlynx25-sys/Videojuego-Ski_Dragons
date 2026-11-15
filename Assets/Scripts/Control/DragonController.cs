using UnityEngine;

/// Controla el movimiento vertical del dragón: cada vez que el jugador
/// pulsa espacio o hace clic, el dragón da un aleteo hacia arriba.
/// Si choca con una torre, avisa al GameManager.
public class DragonController : MonoBehaviour
{
    ///Velocidad hacia arriba que recibe el dragón en cada aleteo.
    [SerializeField] private float fuerzaAleteo = 5f;

    ///Componente de física 2D del dragón.
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
    }

    ///Se llama al chocar con otro collider (las torres).
    private void OnCollisionEnter2D(Collision2D colision)
    {
        Debug.Log("¡Game Over! Chocaste con " + colision.collider.name);
        GameManager.Instancia.TerminarPartida();
    }
}