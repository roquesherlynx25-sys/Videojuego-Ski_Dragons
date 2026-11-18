using UnityEngine;

/// <summary>
/// Controla al dragón en la partida: aleteo, límites de pantalla, choques,
/// habilidad del personaje elegido y reaparición al revivir con el Ángel.
/// </summary>
public class DragonController : MonoBehaviour
{
    [SerializeField] private float fuerzaAleteo = 5f;
    [SerializeField] private float limiteInferior = -5.5f;
    [SerializeField] private float limiteSuperior = 5.5f;
    [SerializeField] private float segundosInvulnerable = 2f;

    private Rigidbody2D rb;
    private Collider2D colision;
    private SpriteRenderer sprite;
    private Color colorBase;
    private float invulnerableHasta;

    /// <summary>Personaje elegido en el menú (Jinete si se juega sin pasar por él).</summary>
    public Personaje Personaje { get; private set; }

    /// <summary>True durante los segundos de protección tras revivir.</summary>
    private bool Invulnerable => Time.unscaledTime < invulnerableHasta;

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

    private void Start()
    {
        GameManager.Instancia.OnRevivir += Reaparecer;
    }

    private void OnDestroy()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.OnRevivir -= Reaparecer;
        }
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

        // Gorgón: con el escudo (o la protección al revivir) atraviesa las torres.
        bool escudo = dragon is DragonGorgon gorgon && gorgon.Dureza.EsInmune;
        bool atraviesa = escudo || Invulnerable;
        colision.isTrigger = atraviesa;

        if (escudo) sprite.color = Color.yellow;
        else if (Invulnerable) sprite.color = new Color(1f, 1f, 1f, 0.5f);
        else sprite.color = colorBase;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * fuerzaAleteo;
        }

        if (transform.position.y < limiteInferior || transform.position.y > limiteSuperior)
        {
            Debug.Log("El dragón salió de la pantalla");
            GameManager.Instancia.TerminarPartida();
        }
    }

    /// <summary>Al revivir: vuelve al centro, sin velocidad y con protección breve.</summary>
    private void Reaparecer()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.linearVelocity = Vector2.zero;
        invulnerableHasta = Time.unscaledTime + segundosInvulnerable;
    }

    private void OnCollisionEnter2D(Collision2D otro) { Chocar(otro.collider.name); }

    // Por si el escudo termina mientras el dragón sigue tocando una torre.
    private void OnCollisionStay2D(Collision2D otro) { Chocar(otro.collider.name); }

    private void Chocar(string nombre)
    {
        if (Invulnerable) return;

        Debug.Log("Choque con " + nombre);
        GameManager.Instancia.TerminarPartida();
    }
}
