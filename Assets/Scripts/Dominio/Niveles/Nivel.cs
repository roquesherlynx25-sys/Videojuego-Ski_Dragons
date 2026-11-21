/// <summary>
/// Clase base abstracta de los niveles del modo Niveles (capas de la
/// atmósfera). Cada nivel define su nombre, cuántos obstáculos hay que
/// superar para completarlo, su color de fondo y qué tan rápido avanzan
/// los obstáculos.
/// </summary>
public abstract class Nivel
{
    /// <summary>Nombre del nivel que se muestra en pantalla.</summary>
    public abstract string Nombre { get; }

    /// <summary>Obstáculos que hay que superar para completar el nivel.</summary>
    public abstract int ObstaculosParaCompletar { get; }

    /// <summary>Color del fondo (cielo) del nivel.</summary>
    public abstract ColorRgb ColorFondo { get; }

    /// <summary>Multiplicador de la velocidad de los obstáculos (1 = normal).</summary>
    public virtual float MultiplicadorVelocidad => 1f;

    public override string ToString()
    {
        return Nombre;
    }
}
