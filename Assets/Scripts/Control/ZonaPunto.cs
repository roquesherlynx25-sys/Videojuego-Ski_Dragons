using UnityEngine;

/// <summary>
/// Zona invisible en el hueco de las torres. Cuando el dragón la
/// atraviesa, suma un punto (una sola vez por par de torres).
/// </summary>
public class ZonaPunto : MonoBehaviour
{
    private bool contado;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (contado) return;

        if (otro.GetComponent<DragonController>() != null)
        {
            contado = true;
            GameManager.Instancia.SumarPunto();
        }
    }
}
