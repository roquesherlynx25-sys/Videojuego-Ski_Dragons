using UnityEngine;
///Mueve un obstáculo hacia la izquierda a velocidad constante
///y lo destruye cuando sale de la pantalla.
public class ObstaculoMovil : MonoBehaviour
{
    ///Unidades por segundo que avanza hacia la izquierda.
    [SerializeField] private float velocidad = 3f;

    ///Posición en X a partir de la cual el obstáculo se elimina.
    [SerializeField] private float limiteIzquierdo = -12f;

    private void Update()
    {
        //Time.deltaTime hace que la velocidad sea la misma en cualquier PC.
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < limiteIzquierdo)
        {
            Destroy(gameObject);
        }
    }
}