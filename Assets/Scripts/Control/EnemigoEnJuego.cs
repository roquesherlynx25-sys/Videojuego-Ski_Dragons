using UnityEngine;

/// <summary>
/// Comportamiento de un enemigo durante la partida. El Grifo vuela recto;
/// el Goblin corre por abajo y, cuando el dragón se acerca, salta en
/// diagonal hacia la altura media. Al tocar al dragón aplica su efecto.
/// </summary>
public class EnemigoEnJuego : MonoBehaviour
{
    private const float AlturaMedia = 0.5f;
    private const float DistanciaSalto = 5f;
    private const float VelocidadSalto = 5f;

    private Enemigo enemigo;
    private float velocidad;
    private bool saltando;
    private DragonController dragon;

    /// <summary>Lo llama la fábrica al crear el enemigo.</summary>
    public void Inicializar(Enemigo enemigoDominio, float velocidadAvance)
    {
        enemigo = enemigoDominio;
        velocidad = velocidadAvance;
    }

    private void Start()
    {
        dragon = FindFirstObjectByType<DragonController>();
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        transform.position += Vector3.left * velocidad * dt;

        if (enemigo is Goblin)
        {
            MoverGoblin(dt);
        }

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    private void MoverGoblin(float dt)
    {
        if (dragon == null) return;

        float distancia = transform.position.x - dragon.transform.position.x;
        if (!saltando && distancia > 0f && distancia < DistanciaSalto)
        {
            saltando = true;
        }

        if (saltando && transform.position.y < AlturaMedia)
        {
            transform.position += Vector3.up * VelocidadSalto * dt;
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        DragonController tocado = otro.GetComponent<DragonController>();
        if (tocado == null) return;

        if (tocado.TocadoPorEnemigo(enemigo))
        {
            Destroy(gameObject);
        }
    }
}
