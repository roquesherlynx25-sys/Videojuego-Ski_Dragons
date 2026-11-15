using UnityEngine;

/// Genera pares de torres a intervalos regulares, con una altura
/// aleatoria para que el hueco cambie de posición.
public class ObstacleSpawner : MonoBehaviour
{
    ///Molde (prefab) del par de torres que se va a copiar.
    [SerializeField] private GameObject prefabTorres;

    ///Segundos entre cada par de torres.
    [SerializeField] private float intervalo = 2f;

    ///Altura mínima del centro del hueco.
    [SerializeField] private float alturaMin = -2f;

    ///Altura máxima del centro del hueco.
    [SerializeField] private float alturaMax = 2f;

    ///Tiempo acumulado desde la última generación.
    private float temporizador;

    private void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= intervalo)
        {
            temporizador = 0f;
            GenerarTorres();
        }
    }

    ///Crea una copia del prefab en una altura aleatoria.
    private void GenerarTorres()
    {
        float y = Random.Range(alturaMin, alturaMax);
        Vector3 posicion = new Vector3(transform.position.x, y, 0f);
        Instantiate(prefabTorres, posicion, Quaternion.identity);
    }
}