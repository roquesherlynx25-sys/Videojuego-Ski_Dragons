using UnityEngine;

/// <summary>
/// Genera pares de torres a intervalos regulares, con una altura
/// aleatoria para que el hueco cambie de posición. Si los obstáculos
/// van más rápido (niveles altos), aparecen en menos tiempo para que
/// la distancia entre torres se mantenga igual.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    /// <summary>Molde (prefab) del par de torres que se va a copiar.</summary>
    [SerializeField] private GameObject prefabTorres;

    /// <summary>Segundos entre cada par de torres (a velocidad normal).</summary>
    [SerializeField] private float intervalo = 2f;

    /// <summary>Altura mínima del centro del hueco.</summary>
    [SerializeField] private float alturaMin = -2f;

    /// <summary>Altura máxima del centro del hueco.</summary>
    [SerializeField] private float alturaMax = 2f;

    /// <summary>Tiempo acumulado desde la última generación.</summary>
    private float temporizador;

    private void Update()
    {
        float factor = GameManager.Instancia != null ? GameManager.Instancia.MultiplicadorVelocidad : 1f;
        temporizador += Time.deltaTime * factor;

        if (temporizador >= intervalo)
        {
            temporizador = 0f;
            GenerarTorres();
        }
    }

    /// <summary>Crea una copia del prefab en una altura aleatoria.</summary>
    private void GenerarTorres()
    {
        float y = Random.Range(alturaMin, alturaMax);
        Vector3 posicion = new Vector3(transform.position.x, y, 0f);
        Instantiate(prefabTorres, posicion, Quaternion.identity);
    }
}
