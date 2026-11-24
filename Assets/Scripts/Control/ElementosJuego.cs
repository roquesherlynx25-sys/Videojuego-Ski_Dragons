using UnityEngine;

/// <summary>
/// Fábrica de los elementos que aparecen durante la partida (enemigos,
/// ovejas y fragmentos de Ángel). Los crea desde código, así no hace falta
/// armar prefabs en el editor.
/// </summary>
public static class ElementosJuego
{
    /// <summary>Crea un enemigo que avanza hacia la izquierda.</summary>
    public static EnemigoEnJuego CrearEnemigo(Enemigo enemigo, Vector3 posicion, float velocidad)
    {
        bool esGrifo = enemigo is Grifo;

        GameObject objeto = new GameObject(enemigo.Nombre);
        objeto.transform.position = posicion;

        Color color = esGrifo ? new Color(0.95f, 0.55f, 0.1f) : new Color(0.25f, 0.75f, 0.25f);
        Vector2 tamano = ArteJuego.AplicarVisual(objeto, esGrifo ? "grifo" : "goblin", color, FormaPlaceholder.Cuadrado, 1f);
        PrepararTrigger(objeto, tamano, 3);

        EnemigoEnJuego comportamiento = objeto.AddComponent<EnemigoEnJuego>();
        comportamiento.Inicializar(enemigo, velocidad);
        return comportamiento;
    }

    /// <summary>Crea una oveja que avanza con el mundo.</summary>
    public static OvejaEnJuego CrearOveja(Vector3 posicion, int recompensa)
    {
        GameObject objeto = new GameObject("Oveja");
        objeto.transform.position = posicion;

        ArteJuego.AplicarVisual(objeto, "oveja", new Color(0.95f, 0.95f, 0.9f), FormaPlaceholder.Circulo, 1f);
        objeto.GetComponent<SpriteRenderer>().sortingOrder = 2;

        OvejaEnJuego comportamiento = objeto.AddComponent<OvejaEnJuego>();
        comportamiento.Inicializar(new Oveja(recompensa));
        return comportamiento;
    }

    /// <summary>Crea un fragmento de Ángel que avanza con el mundo.</summary>
    public static FragmentoAngelEnJuego CrearFragmento(Vector3 posicion)
    {
        GameObject objeto = new GameObject("FragmentoAngel");
        objeto.transform.position = posicion;

        Vector2 tamano = ArteJuego.AplicarVisual(objeto, "fragmento_angel", new Color(0.6f, 0.85f, 1f), FormaPlaceholder.Circulo, 0.7f);
        PrepararTrigger(objeto, tamano, 3);

        return objeto.AddComponent<FragmentoAngelEnJuego>();
    }

    /// <summary>Le pone collider de tipo trigger y un cuerpo cinemático para detectar al dragón.</summary>
    private static void PrepararTrigger(GameObject objeto, Vector2 tamano, int ordenDibujo)
    {
        BoxCollider2D colision = objeto.AddComponent<BoxCollider2D>();
        colision.size = tamano;
        colision.isTrigger = true;

        Rigidbody2D cuerpo = objeto.AddComponent<Rigidbody2D>();
        cuerpo.bodyType = RigidbodyType2D.Kinematic;

        objeto.GetComponent<SpriteRenderer>().sortingOrder = ordenDibujo;
    }
}
