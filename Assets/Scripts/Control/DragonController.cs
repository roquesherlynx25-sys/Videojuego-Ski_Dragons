using UnityEngine;

/// <summary>
/// Controla al dragón en la partida: aleteo, límites de pantalla, choques,
/// habilidad del personaje (tecla E), Runa de Escudo (tecla Q), vidas extra
/// del Gorgón, efecto de los enemigos y reaparición al revivir con el Ángel.
/// </summary>
public class DragonController : MonoBehaviour
{
    [SerializeField] private float fuerzaAleteo = 5f;
    [SerializeField] private float limiteInferior = -5.5f;
    [SerializeField] private float limiteSuperior = 5.5f;
    [SerializeField] private float segundosInvulnerable = 2f; // tras revivir

    private Rigidbody2D rb;
    private Collider2D colision;
    private SpriteRenderer sprite;
    private SpriteRenderer spriteVisual;
    private Color colorBase;
    private float invulnerableHasta;

    /// <summary>Personaje elegido en el menú (Jinete si se juega sin pasar por él).</summary>
    public Personaje Personaje { get; private set; }

    /// <summary>True durante la protección tras revivir o tras resistir un golpe.</summary>
    private bool Invulnerable => Time.unscaledTime < invulnerableHasta;

    /// <summary>Dibujo que se está usando (el real si existe, si no el cuadro).</summary>
    private SpriteRenderer Activo => spriteVisual != null ? spriteVisual : sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colision = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Personaje = SesionJuego.Jugador.PersonajeSeleccionado
                    ?? new Jinete(SesionJuego.NombreUsuario);

        // Dibujo real si existe en Resources/Sprites; si no, un color por personaje.
        string arte = "jinete";
        if (Personaje is DragonAndary) arte = "dragon_andary";
        else if (Personaje is DragonGorgon) arte = "dragon_gorgon";
        spriteVisual = ArteJuego.CrearHijoVisual(transform, arte, 1.2f);

        if (spriteVisual != null)
        {
            sprite.enabled = false;
            colorBase = Color.white;
        }
        else if (Personaje is DragonAndary) colorBase = new Color(0.5f, 0.8f, 1f);
        else if (Personaje is DragonGorgon) colorBase = new Color(0.6f, 1f, 0.6f);
        else colorBase = Color.white;

        Activo.color = colorBase;
    }

    private void Start()
    {
        GameManager.Instancia.OnRevivir += Reaparecer;

        // El bioma alarga la recarga de las habilidades.
        float factor = GameManager.Instancia.Modo.FactorRecarga;

        Dragon dragon = Personaje as Dragon;
        if (dragon != null) dragon.Habilidad.MultiplicadorRecarga = factor;

        RunaEscudo runa = SesionJuego.Jugador.ObtenerEquipado<RunaEscudo>();
        if (runa != null) runa.Escudo.MultiplicadorRecarga = factor;
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
        RunaEscudo runa = SesionJuego.Jugador.ObtenerEquipado<RunaEscudo>();

        // Tiempo real: la duración y la recarga no deben depender del x0.5.
        if (dragon != null)
        {
            dragon.Actualizar(Time.unscaledDeltaTime);
            if (Input.GetKeyDown(KeyCode.E)) dragon.UsarHabilidad();
        }

        if (runa != null)
        {
            runa.Escudo.Actualizar(Time.unscaledDeltaTime);
            if (Input.GetKeyDown(KeyCode.Q)) runa.Escudo.Activar();
        }

        // Andary reduce el tiempo del juego mientras su habilidad está activa.
        Time.timeScale = (dragon is DragonAndary andary) ? andary.Tiempo.EscalaTiempo : 1f;

        // Con el escudo (Gorgón o Runa) o la protección, atraviesa lo que lo toque.
        bool escudo = (dragon is DragonGorgon gorgon && gorgon.Dureza.EsInmune)
                      || (runa != null && runa.Escudo.EsInmune);
        colision.isTrigger = escudo || Invulnerable;

        if (escudo) Activo.color = Color.yellow;
        else if (Invulnerable) Activo.color = new Color(1f, 1f, 1f, 0.5f);
        else Activo.color = colorBase;

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

    /// <summary>Al revivir: vuelve al centro, con vida, sin velocidad y con protección breve.</summary>
    private void Reaparecer()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.linearVelocity = Vector2.zero;
        Personaje.Revivir();
        invulnerableHasta = Time.unscaledTime + segundosInvulnerable;
    }

    private void OnCollisionEnter2D(Collision2D otro) { RecibirGolpe(otro.collider.name); }

    // Por si el escudo termina mientras el dragón sigue tocando una torre.
    private void OnCollisionStay2D(Collision2D otro) { RecibirGolpe(otro.collider.name); }

    /// <summary>
    /// Un obstáculo o enemigo golpea al dragón. Si le quedan vidas (Gorgón)
    /// resiste el golpe y gana unos segundos de protección; si no, pierde.
    /// </summary>
    private void RecibirGolpe(string causa)
    {
        if (GameManager.Instancia.Estado != EstadoPartida.Jugando) return;
        if (Invulnerable) return;

        Personaje.RecibirGolpe();

        if (Personaje.EstaVivo)
        {
            float proteccion = (Personaje is Dragon d) ? d.SegundosInvulnerableTrasGolpe : 0.5f;
            invulnerableHasta = Time.unscaledTime + proteccion;
            GameManager.Instancia.NotificarMensaje("¡Resististe el golpe! Vidas: " + Personaje.Vidas);
            return;
        }

        Debug.Log("Perdiste por: " + causa);
        GameManager.Instancia.TerminarPartida();
    }

    /// <summary>
    /// Un enemigo toca al dragón. Devuelve true si el contacto contó (y el
    /// enemigo desaparece) y false si el dragón estaba protegido.
    /// </summary>
    public bool TocadoPorEnemigo(Enemigo enemigo)
    {
        if (GameManager.Instancia.Estado != EstadoPartida.Jugando) return false;
        if (Invulnerable || colision.isTrigger) return false;

        EfectoContacto efecto = enemigo.AlTocar(SesionJuego.Jugador);

        if (efecto == EfectoContacto.RoboMonedas)
        {
            int robadas = (enemigo is Grifo grifo) ? grifo.UltimoRobo : 0;
            GameManager.Instancia.NotificarMensaje("¡" + enemigo.Nombre + " te robó " + robadas + " monedas!");
        }
        else if (efecto == EfectoContacto.MataDragon)
        {
            RecibirGolpe(enemigo.Nombre);
        }

        return true;
    }
}
