using UnityEngine;

/// <summary>
/// Mueve un obstáculo hacia la izquierda y lo destruye cuando sale de la
/// pantalla. La velocidad es la base del juego por el factor del modo
/// (nivel, bioma o escalón de dificultad).
/// </summary>
public class ObstaculoMovil : MonoBehaviour
{
    /// <summary>Posición en X a partir de la cual el obstáculo se elimina.</summary>
    [SerializeField] private float limiteIzquierdo = -12f;

    private void Update()
    {
        float factor = GameManager.Instancia != null ? GameManager.Instancia.MultiplicadorVelocidad : 1f;

        // Time.deltaTime hace que la velocidad sea la misma en cualquier PC.
        transform.Translate(Vector3.left * ModoJuego.VelocidadBase * factor * Time.deltaTime);

        if (transform.position.x < limiteIzquierdo)
        {
            Destroy(gameObject);
        }
    }
}
