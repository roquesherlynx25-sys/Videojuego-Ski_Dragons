using UnityEngine;

/// <summary>
/// Moneda que viaja dentro del hueco de las torres. Al tocarla el dragón,
/// el GameManager la suma y la moneda desaparece. Va como hija del
/// prefab ParTorres y necesita un Collider2D con Is Trigger activado.
/// </summary>
public class MonedaRecogible : MonoBehaviour
{
    /// <summary>Altura mínima (dentro del hueco) donde puede aparecer.</summary>
    [SerializeField] private float alturaMinima = -1.5f;

    /// <summary>Altura máxima (dentro del hueco) donde puede aparecer.</summary>
    [SerializeField] private float alturaMaxima = 1.5f;

    private Moneda moneda;

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

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<DragonController>() == null) return;

        GameManager.Instancia.RecogerMoneda(moneda);
        Destroy(gameObject);
    }
}
