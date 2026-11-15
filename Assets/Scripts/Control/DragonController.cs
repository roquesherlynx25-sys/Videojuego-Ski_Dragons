using UnityEngine;

///Controla el movimiento vertical del dragón: cada vez que el jugador
///pulsa espacio o hace clic, el dragón da un aleteo hacia arriba.
///La gravedad lo hace caer gracias al Rigidbody2D.
public class DragonController : MonoBehaviour
{
    ///Velocidad hacia arriba que recibe el dragón en cada aleteo.</summary>
    [SerializeField] private float fuerzaAleteo = 5f;

    ///Componente de física 2D del dragón (se obtiene en Awake).</summary>
    private Rigidbody2D rb;

    /// Se ejecuta una sola vez al crear el objeto, antes de Start.
    /// Aquí guardamos la referencia al Rigidbody2D para usarla después.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    ///Se ejecuta en cada fotograma. Detecta la entrada del jugador
    ///y aplica el aleteo.

    private void Update()
    {
        //GetKeyDown / GetMouseButtonDown son verdaderos solo en el
        //fotograma exacto en que se pulsa (no mientras se mantiene).
        bool aleteo = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (aleteo)
        {
            //Se asigna la velocidad en lugar de sumarla, para que cada
            //aleteo se sienta igual sin importar qué tan rápido caía.
            rb.linearVelocity = Vector2.up * fuerzaAleteo;
        }
    }
    ///Se llama automáticamente cuando el dragón choca con otro collider.
    ///Por ahora solo muestra un mensaje y detiene el juego.
    private void OnCollisionEnter2D(Collision2D colision)
    {
        Debug.Log("¡Game Over! Chocaste con " + colision.collider.name);
        Time.timeScale = 0f; // Congela el juego
    }
}