using UnityEngine;

/// <summary>
/// Genera las columnas y todo lo que aparece entre ellas: enemigos,
/// fragmentos de Ángel y ovejas. La distancia entre columnas y la
/// frecuencia de los extras las dicta el modo de juego (nivel o bioma).
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    /// <summary>Molde (prefab) del par de torres que se va a copiar.</summary>
    [SerializeField] private GameObject prefabTorres;

    /// <summary>Altura mínima del centro del hueco.</summary>
    [SerializeField] private float alturaMin = -2f;

    /// <summary>Altura máxima del centro del hueco.</summary>
    [SerializeField] private float alturaMax = 2f;

    private float distanciaAcumulada;
    private int columnasDesdeEnemigo;
    private int columnasDesdeFragmento;
    private int columnasDesdeOveja;
    private int proximoEnemigo;
    private int proximoFragmento;
    private int proximaOveja;

    private void Start()
    {
        proximoEnemigo = ElegirColumnasEnemigo(GameManager.Instancia.Modo);
        proximoFragmento = Random.Range(8, 12);
        proximaOveja = Random.Range(4, 7);
    }

    private void Update()
    {
        GameManager gm = GameManager.Instancia;
        if (gm == null) return;

        ModoJuego modo = gm.Modo;

        // Se mide la distancia recorrida: así la separación entre columnas se
        // mantiene igual aunque cambie la velocidad.
        float velocidad = ModoJuego.VelocidadBase * modo.MultiplicadorVelocidad;
        distanciaAcumulada += velocidad * Time.deltaTime;

        if (distanciaAcumulada >= modo.SeparacionColumnas)
        {
            distanciaAcumulada -= modo.SeparacionColumnas;
            GenerarColumna(modo);
        }
    }

    private void GenerarColumna(ModoJuego modo)
    {
        float y = Random.Range(alturaMin, alturaMax);
        Vector3 posicion = new Vector3(transform.position.x, y, 0f);
        Instantiate(prefabTorres, posicion, Quaternion.identity);

        GenerarExtras(modo);
    }

    /// <summary>Genera los extras a medio camino entre esta columna y la siguiente.</summary>
    private void GenerarExtras(ModoJuego modo)
    {
        float xMedio = transform.position.x + modo.SeparacionColumnas / 2f;

        columnasDesdeEnemigo++;
        columnasDesdeFragmento++;
        columnasDesdeOveja++;

        if (modo.HayEnemigos && columnasDesdeEnemigo >= proximoEnemigo)
        {
            columnasDesdeEnemigo = 0;
            proximoEnemigo = ElegirColumnasEnemigo(modo);
            GenerarEnemigo(modo, xMedio - 1.2f);
        }

        if (columnasDesdeFragmento >= proximoFragmento)
        {
            columnasDesdeFragmento = 0;
            proximoFragmento = Random.Range(8, 12);
            ElementosJuego.CrearFragmento(new Vector3(xMedio, Random.Range(-2.5f, 2.5f), 0f));
        }

        if (modo.HayOvejas && columnasDesdeOveja >= proximaOveja)
        {
            columnasDesdeOveja = 0;
            proximaOveja = Random.Range(4, 7);
            ElementosJuego.CrearOveja(new Vector3(xMedio + 1.2f, Random.Range(-3.5f, -1.5f), 0f), modo.RecompensaOveja);
        }
    }

    private void GenerarEnemigo(ModoJuego modo, float x)
    {
        bool esGrifo = Random.value < modo.ProbabilidadGrifo;
        Enemigo enemigo = esGrifo ? (Enemigo)new Grifo() : new Goblin();

        // El Grifo vuela a una altura al azar; el Goblin corre por el suelo.
        float y = esGrifo ? Random.Range(-2.5f, 3f) : -4.2f;
        ElementosJuego.CrearEnemigo(enemigo, new Vector3(x, y, 0f), modo.VelocidadEnemigo);
    }

    private static int ElegirColumnasEnemigo(ModoJuego modo)
    {
        return Random.Range(modo.MinColumnasEnemigo, modo.MaxColumnasEnemigo + 1);
    }
}
