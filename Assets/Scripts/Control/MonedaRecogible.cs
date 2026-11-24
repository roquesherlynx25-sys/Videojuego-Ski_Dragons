using UnityEngine;

/// <summary>
/// Moneda que viaja dentro del hueco de las torres. Al tocarla el dragón,
/// el GameManager la suma y la moneda desaparece. Con la Runa de Imán
/// equipada, las monedas cercanas se acercan solas al dragón.
/// Va como hija del prefab ParTorres y necesita un Collider2D con Is Trigger.
/// </summary>
public class MonedaRecogible : MonoBehaviour
{
    /// <summary>Altura mínima (dentro del hueco) donde puede aparecer.</summary>
    [SerializeField] private float alturaMinima = -1.5f;

    /// <summary>Altura máxima (dentro del hueco) donde puede aparecer.</summary>
    [SerializeField] private float alturaMaxima = 1.5f;

    private Moneda moneda;
    private DragonController dragon;

    private void Awake()
    {
        moneda = new Moneda(1);
    }

    private void Start()
    {
        // Cada moneda aparece a una altura distinta dentro del hueco.
        Vector3 posicion = transform.localPosition;
        posicion.y = Random.Range(alturaMinima, alturaMaxima);
        transform.localPosition = posicion;
    }

    private void Update()
    {
        if (SesionJuego.Jugador.ObtenerEquipado<RunaImanCodicia>() == null) return;

        if (dragon == null)
        {
            dragon = FindFirstObjectByType<DragonController>();
            if (dragon == null) return;
        }

        Vector3 haciaElDragon = dragon.transform.position - transform.position;
        if (haciaElDragon.magnitude < RunaImanCodicia.RadioIman)
        {
            transform.position += haciaElDragon.normalized * 6f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<DragonController>() == null) return;

        GameManager.Instancia.RecogerMoneda(moneda);
        Destroy(gameObject);
    }
}
