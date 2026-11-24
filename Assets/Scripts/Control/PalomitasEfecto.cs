using UnityEngine;

/// <summary>
/// Efecto visual de la oveja al explotar «a modo de palomita»: varios
/// granos claros salen disparados y caen. Se crea y se destruye solo.
/// </summary>
public class PalomitasEfecto : MonoBehaviour
{
    private const int Cantidad = 14;
    private const float Duracion = 0.8f;

    private readonly Transform[] granos = new Transform[Cantidad];
    private readonly Vector2[] velocidades = new Vector2[Cantidad];
    private float tiempo;

    /// <summary>Crea la explosión de palomitas en una posición.</summary>
    public static void Crear(Vector3 posicion)
    {
        GameObject objeto = new GameObject("Palomitas");
        objeto.transform.position = posicion;
        objeto.AddComponent<PalomitasEfecto>();
    }

    private void Start()
    {
        for (int i = 0; i < Cantidad; i++)
        {
            GameObject grano = new GameObject("Grano");
            grano.transform.SetParent(transform, false);
            grano.transform.localScale = new Vector3(0.25f, 0.25f, 1f);

            SpriteRenderer render = grano.AddComponent<SpriteRenderer>();
            render.sprite = ArteJuego.Placeholder(FormaPlaceholder.Circulo);
            render.color = new Color(1f, 0.95f, 0.7f);
            render.sortingOrder = 6;

            float angulo = Random.Range(0f, Mathf.PI * 2f);
            float fuerza = Random.Range(2f, 5f);
            velocidades[i] = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo) + 0.5f) * fuerza;
            granos[i] = grano.transform;
        }
    }

    private void Update()
    {
        tiempo += Time.deltaTime;

        for (int i = 0; i < Cantidad; i++)
        {
            if (granos[i] == null) continue;

            velocidades[i].y -= 9f * Time.deltaTime;
            granos[i].localPosition += (Vector3)(velocidades[i] * Time.deltaTime);
        }

        if (tiempo >= Duracion)
        {
            Destroy(gameObject);
        }
    }
}
