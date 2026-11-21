/// <summary>
/// Color de fondo de un nivel (rojo, verde y azul entre 0 y 1).
/// Es un struct (tipo por valor) y así el dominio no depende de UnityEngine.
/// </summary>
public readonly struct ColorRgb
{
    /// <summary>Componente roja (0 a 1).</summary>
    public float R { get; }

    /// <summary>Componente verde (0 a 1).</summary>
    public float G { get; }

    /// <summary>Componente azul (0 a 1).</summary>
    public float B { get; }

    public ColorRgb(float r, float g, float b)
    {
        R = Limitar(r);
        G = Limitar(g);
        B = Limitar(b);
    }

    private static float Limitar(float valor)
    {
        if (valor < 0f) return 0f;
        if (valor > 1f) return 1f;
        return valor;
    }

    public override string ToString()
    {
        return $"({R:0.00}, {G:0.00}, {B:0.00})";
    }
}
