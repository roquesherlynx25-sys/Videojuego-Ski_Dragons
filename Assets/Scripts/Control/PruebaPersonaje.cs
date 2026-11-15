using UnityEngine;

/// <summary>Script temporal para comprobar las clases de personajes.</summary>
public class PruebaPersonajes : MonoBehaviour
{
    private void Start()
    {
        Personaje jinete = new Jinete("Aldric");
        Debug.Log(jinete); // usa ToString()
        Debug.Log("¿Tiene habilidad? " + jinete.TieneHabilidad);

        jinete.RecibirGolpe(); // sobrecarga sin parámetros
        Debug.Log(jinete);

        Personaje resistente = new Jinete("Brisa", 3);
        resistente.RecibirGolpe(2); // sobrecarga con daño
        Debug.Log(resistente);

        try
        {
            new Jinete("   ");
        }
        catch (System.ArgumentException e)
        {
            Debug.Log("Error esperado: " + e.Message);
        }
    }
}