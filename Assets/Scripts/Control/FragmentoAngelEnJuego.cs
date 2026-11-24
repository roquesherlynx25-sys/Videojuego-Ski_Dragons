using UnityEngine;

/// <summary>
/// Fragmento de Ángel que viaja con el mundo. Al tocarlo el dragón se suma
/// al contador (3 fragmentos = un tótem que revive).
/// </summary>
public class FragmentoAngelEnJuego : MonoBehaviour
{
    private void Update()
    {
        float factor = GameManager.Instancia != null ? GameManager.Instancia.MultiplicadorVelocidad : 1f;
        transform.position += Vector3.left * ModoJuego.VelocidadBase * factor * Time.deltaTime;

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<DragonController>() == null) return;

        GameManager.Instancia.RecogerFragmentoAngel();
        Destroy(gameObject);
    }
}
