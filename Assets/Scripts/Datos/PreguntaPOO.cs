using System;

/// <summary>
/// Pregunta de opción múltiple sobre Programación Orientada a Objetos.
/// Se usa para ganar el fragmento que falta del Ángel.
/// </summary>
public class PreguntaPOO
{
    private readonly string[] opciones;

    /// <summary>Enunciado de la pregunta.</summary>
    public string Texto { get; }

    /// <summary>Posición (desde 0) de la respuesta correcta.</summary>
    public int IndiceCorrecto { get; }

    /// <summary>Cuántas opciones tiene la pregunta.</summary>
    public int CantidadOpciones => opciones.Length;

    public PreguntaPOO(string texto, string[] opciones, int indiceCorrecto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("La pregunta necesita un enunciado.");
        if (opciones == null || opciones.Length < 2)
            throw new ArgumentException("Se necesitan al menos 2 opciones.");
        if (indiceCorrecto < 0 || indiceCorrecto >= opciones.Length)
            throw new ArgumentOutOfRangeException(nameof(indiceCorrecto));

        Texto = texto;
        this.opciones = (string[])opciones.Clone();
        IndiceCorrecto = indiceCorrecto;
    }

    /// <summary>Devuelve el texto de una opción.</summary>
    public string ObtenerOpcion(int indice)
    {
        if (indice < 0 || indice >= opciones.Length)
            throw new ArgumentOutOfRangeException(nameof(indice));

        return opciones[indice];
    }

    /// <summary>Indica si la opción elegida es la correcta.</summary>
    public bool EsCorrecta(int indiceElegido)
    {
        return indiceElegido == IndiceCorrecto;
    }

    public override string ToString()
    {
        return Texto;
    }
}
