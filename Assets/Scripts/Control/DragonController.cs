using UnityEngine;

/// <summary>
/// Controla al dragón en la partida: aleteo, límites de pantalla, choques
/// y uso de la habilidad del personaje elegido en el menú.
/// </summary>
public class DragonController : MonoBehaviour
{
    [SerializeField] private float fuerzaAleteo = 5f;
    [SerializeField] private float limiteInferior = -5.5f;
    [SerializeField] private float limiteSuperior = 5.5f;

    private Rigidbody2D rb;
    private Collider2D colision;
    private SpriteRenderer sprite;
    private Color colorBase;

    /// <summary>Personaje elegido en el menú (Jinete si se juega sin pasar por él).</summary>
    public Personaje Personaje { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colision = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Personaje = SesionJuego.Jugador.PersonajeSeleccionado
                    ?? new Jinete(SesionJuego.NombreUsuario);

        // Un color distinto para reconocer a cada personaje.
        if (Personaje is DragonAndary) colorBase = new Color(0.5f, 0.8f, 1f);
        else if (Personaje is DragonGorgon) colorBase = new Color(0.6f, 1f, 0.6f);
        else colorBase = Color.white;
        sprite.color = colorBase;
    }

    private void Update()
    {
        if (GameManager.Instancia.Estado != EstadoPartida.Jugando) return;

        Dragon dragon = Personaje as Dragon;

        if (dragon != null)
        {
            // Tiempo real: la duración y la recarga no deben depender del x0.5.
            dragon.Actualizar(Time.unscaledDeltaTime);

            if (Input.GetKeyDown(KeyCode.E))
            {
                dragon.UsarHabilidad();
            }
        }

        // Andary reduce el tiempo del juego mientras su habilidad está activa.
        Time.timeScale = (dragon is DragonAndary andary) ? andary.Tiempo.EscalaTiempo : 1f;

        // Gorgón: con el escudo, el collider se vuelve trigger y atraviesa las torres.
        bool inmune = dragon is DragonGorgon gorgon && gorgon.Dureza.EsInmune;
        colision.isTrigger = inmune;
        sprite.color = inmune ? Color.yellow : colorBase;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * fuerzaAleteo;
        }

        if (transform.position.y < limiteInferior || transform.position.y > limiteSuperior)
        {
            Debug.Log("¡Game Over! El dragón salió de la pantalla");
            GameManager.Instancia.TerminarPartida();
        }
    }

    private void OnCollisionEnter2D(Collision2D otro) { Chocar(otro); }

    // Por si el escudo termina mientras el dragón sigue tocando una torre.
    private void OnCollisionStay2D(Collision2D otro) { Chocar(otro); }

    private void Chocar(Collision2D otro)
    {
        Debug.Log("¡Game Over! Chocaste con " + otro.collider.name);
        GameManager.Instancia.TerminarPartida();
    }
}