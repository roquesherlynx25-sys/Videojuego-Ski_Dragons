using UnityEngine;

/// <summary>Script temporal para comprobar dragones y habilidades.</summary>
public class PruebaDragones : MonoBehaviour
{
    private void Start()
    {
        // Andary: activar, recarga y tiempo simulado
        DragonAndary andary = new DragonAndary();
        Debug.Log(andary);
        Debug.Log("Activar: " + andary.Habilidad.Activar());
        Debug.Log("Escala de tiempo: " + andary.Tiempo.EscalaTiempo);
        Debug.Log("Activar otra vez: " + andary.Habilidad.Activar());
        andary.Actualizar(3f);
        Debug.Log("Activa: " + andary.Habilidad.EstaActiva + " | lista: " + andary.Habilidad.EstaLista);
        andary.Actualizar(12f);
        Debug.Log("Lista tras recarga: " + andary.Habilidad.EstaLista);

        // Gorgón con habilidad de nivel 3 (escudo de 4 s)
        DragonGorgon gorgon = new DragonGorgon(3);
        Debug.Log(gorgon);
        gorgon.UsarHabilidad();
        Debug.Log("Inmune: " + gorgon.Dureza.EsInmune);
        gorgon.Actualizar(10f);
        Debug.Log("Inmune tras 10 s: " + gorgon.Dureza.EsInmune);

        // Polimorfismo: la misma lista, comportamientos distintos
        Personaje[] personajes = { new Jinete("Aldric"), new DragonAndary(), new DragonGorgon() };
        foreach (Personaje p in personajes)
        {
            Debug.Log(p.Rol + " - ¿tiene habilidad? " + p.TieneHabilidad);
        }
    }
}