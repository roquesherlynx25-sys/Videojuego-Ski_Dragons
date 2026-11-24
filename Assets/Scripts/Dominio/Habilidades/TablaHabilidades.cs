using System;

/// <summary>
/// Tabla de niveles de las habilidades (decisión final del equipo):
/// recarga, duración del efecto de Tiempo y de Escudo, golpes de columna
/// que resiste el Dragón de Dureza y precio de cada mejora.
/// Todos los datos viven aquí para ajustarlos en un solo lugar.
/// </summary>
public static class TablaHabilidades
{
    /// <summary>Nivel inicial de toda habilidad.</summary>
    public const int NivelMinimo = 1;

    /// <summary>Nivel máximo de toda habilidad.</summary>
    public const int NivelMaximo = 5;

    private static readonly float[] recarga = { 15f, 13.5f, 12f, 10.5f, 8.5f };
    private static readonly float[] duracionTiempo = { 2.5f, 3f, 3.5f, 4f, 5f };
    private static readonly float[] duracionEscudo = { 3f, 3.5f, 4.5f, 5.5f, 7f };
    private static readonly int[] golpesResistidos = { 1, 1, 2, 2, 3 };
    private static readonly float[] invulnerabilidadTrasGolpe = { 0.5f, 1f, 0.5f, 1.5f, 0.5f };
    private static readonly int[] precioDeNivel = { 0, 250, 600, 1300, 2500 };

    private static int Indice(int nivel)
    {
        return Math.Clamp(nivel, NivelMinimo, NivelMaximo) - 1;
    }

    /// <summary>Segundos de recarga a ese nivel.</summary>
    public static float Recarga(int nivel) { return recarga[Indice(nivel)]; }

    /// <summary>Segundos que dura la habilidad Tiempo (Andary).</summary>
    public static float DuracionTiempo(int nivel) { return duracionTiempo[Indice(nivel)]; }

    /// <summary>Segundos que dura el escudo (Dureza y Runa de Escudo).</summary>
    public static float DuracionEscudo(int nivel) { return duracionEscudo[Indice(nivel)]; }

    /// <summary>Golpes de columna extra que resiste el Dragón de Dureza.</summary>
    public static int GolpesResistidos(int nivel) { return golpesResistidos[Indice(nivel)]; }

    /// <summary>Segundos de protección después de resistir un golpe.</summary>
    public static float InvulnerabilidadTrasGolpe(int nivel) { return invulnerabilidadTrasGolpe[Indice(nivel)]; }

    /// <summary>Monedas que cuesta subir la habilidad hasta ese nivel.</summary>
    public static int PrecioDeNivel(int nivel) { return precioDeNivel[Indice(nivel)]; }
}
