using System.Collections.Generic;
using UnityEngine;

/// <summary>Forma del dibujo temporal que se usa cuando todavía no hay arte.</summary>
public enum FormaPlaceholder
{
    Cuadrado,
    Circulo
}

/// <summary>
/// Entrega los dibujos del juego. Si existe una imagen en
/// Assets/Resources/Sprites/NOMBRE.png la usa; si no, dibuja una figura de
/// color. Así el juego funciona sin arte y se vuelve bonito al soltar los
/// PNG en esa carpeta, sin tocar el código.
/// </summary>
public static class ArteJuego
{
    private static readonly Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

    /// <summary>Carga Resources/Sprites/NOMBRE. Devuelve null si no existe.</summary>
    public static Sprite Cargar(string nombre)
    {
        string clave = "res:" + nombre;
        Sprite sprite;
        if (cache.TryGetValue(clave, out sprite)) return sprite;

        sprite = Resources.Load<Sprite>("Sprites/" + nombre);
        cache[clave] = sprite;
        return sprite;
    }

    /// <summary>Figura blanca de 1 x 1 unidad (se tiñe con el color del SpriteRenderer).</summary>
    public static Sprite Placeholder(FormaPlaceholder forma)
    {
        string clave = "ph:" + forma;
        Sprite sprite;
        if (cache.TryGetValue(clave, out sprite) && sprite != null) return sprite;

        const int lado = 32;
        Texture2D textura = new Texture2D(lado, lado, TextureFormat.RGBA32, false);
        textura.filterMode = FilterMode.Point;

        float centro = (lado - 1) / 2f;
        float radio = lado / 2f;

        for (int y = 0; y < lado; y++)
        {
            for (int x = 0; x < lado; x++)
            {
                float dx = x - centro;
                float dy = y - centro;
                bool dentro = forma == FormaPlaceholder.Cuadrado || (dx * dx + dy * dy <= radio * radio);
                textura.SetPixel(x, y, dentro ? Color.white : new Color(1f, 1f, 1f, 0f));
            }
        }

        textura.Apply();
        sprite = Sprite.Create(textura, new Rect(0f, 0f, lado, lado), new Vector2(0.5f, 0.5f), lado);
        cache[clave] = sprite;
        return sprite;
    }

    /// <summary>
    /// Le pone dibujo a un objeto (el de Resources o el temporal) y lo escala
    /// para que mida «altura» unidades de alto. Devuelve el tamaño local del
    /// sprite, útil para ajustar el collider.
    /// </summary>
    public static Vector2 AplicarVisual(GameObject objeto, string nombre, Color colorTemporal, FormaPlaceholder forma, float altura)
    {
        SpriteRenderer render = objeto.GetComponent<SpriteRenderer>();
        if (render == null) render = objeto.AddComponent<SpriteRenderer>();

        Sprite real = Cargar(nombre);
        if (real != null)
        {
            render.sprite = real;
            render.color = Color.white;
        }
        else
        {
            render.sprite = Placeholder(forma);
            render.color = colorTemporal;
        }

        Vector2 tamano = render.sprite.bounds.size;
        float escala = altura / tamano.y;
        objeto.transform.localScale = new Vector3(escala, escala, 1f);
        return tamano;
    }

    /// <summary>
    /// Crea un objeto hijo con el dibujo real (si existe) para el dragón, sin
    /// alterar su collider. Devuelve null si no hay imagen.
    /// </summary>
    public static SpriteRenderer CrearHijoVisual(Transform padre, string nombre, float altura)
    {
        Sprite real = Cargar(nombre);
        if (real == null) return null;

        GameObject hijo = new GameObject("Visual");
        hijo.transform.SetParent(padre, false);

        SpriteRenderer render = hijo.AddComponent<SpriteRenderer>();
        render.sprite = real;
        render.sortingOrder = 5;

        float escala = altura / real.bounds.size.y;
        hijo.transform.localScale = new Vector3(escala, escala, 1f);
        return render;
    }
}
