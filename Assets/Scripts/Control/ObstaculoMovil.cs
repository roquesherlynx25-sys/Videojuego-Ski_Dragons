using UnityEngine;

/// <summary>
/// Mueve un obstáculo hacia la izquierda a velocidad constante
/// y lo destruye cuando sale de la pantalla. La velocidad se multiplica
/// por el factor del modo/nivel actual.
/// </summary>
public class ObstaculoMovil : MonoBehaviour
{
    /// <summary>Unidades por segundo que avanza hacia la izquierda.</summary>
    [SerializeField] private float velocidad = 3f;

    /// <summary>Posición en X a partir de la cual el obstáculo se elimina.</summary>
    [SerializeField] private float limiteIzquierdo = -12f;

    private void Update()
    {
        float factor = GameManager.Instancia != null ? GameManager.Instancia.MultiplicadorVelocidad : 1f;

        // Time.deltaTime hace que la velocidad sea la misma en cualquier PC.
        transform.Translate(Vector3.left * velocidad * factor * Time.deltaTime);

        if (transform.position.x < limiteIzquierdo)
        {
            Destroy(gameObject);
        }
    }
}
