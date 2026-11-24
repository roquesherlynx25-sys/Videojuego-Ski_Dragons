/// <summary>
/// Clase base abstracta de los biomas del modo Biomas. Cada bioma define
/// su dificultad: velocidad, separación de columnas, enemigos, factor de
/// recarga de las habilidades, recompensa de las ovejas y precio de
/// desbloqueo.
/// </summary>
public abstract class Bioma
{
    /// <summary>Tipo del bioma (clave para guardarlo).</summary>
    public abstract TipoBioma Tipo { get; }

    /// <summary>Nombre que se muestra en pantalla.</summary>
    public abstract string Nombre { get; }

    /// <summary>Monedas para desbloquearlo (0 = incluido).</summary>
    public abstract int PrecioDesbloqueo { get; }

    /// <summary>Color del cielo del bioma.</summary>
    public abstract ColorRgb ColorFondo { get; }

    /// <summary>Multiplicador de la velocidad de las columnas.</summary>
    public virtual float MultiplicadorVelocidad => 1f;

    /// <summary>Multiplicador de la velocidad de los enemigos.</summary>
    public virtual float FactorVelocidadEnemigo => 1.1f;

    /// <summary>Distancia entre columnas.</summary>
    public virtual float SeparacionColumnas => 6.5f;

    /// <summary>Mínimo de columnas entre un enemigo y el siguiente.</summary>
    public virtual int MinColumnasEnemigo => 6;

    /// <summary>Máximo de columnas entre un enemigo y el siguiente.</summary>
    public virtual int MaxColumnasEnemigo => 8;

    /// <summary>Probabilidad (0 a 1) de que el enemigo sea el Grifo; si no, es el Goblin.</summary>
    public virtual float ProbabilidadGrifo => 1f;

    /// <summary>Factor que alarga la recarga de las habilidades (1.0 = normal).</summary>
    public virtual float FactorRecarga => 1f;

    /// <summary>Monedas que da una oveja al explotar.</summary>
    public virtual int RecompensaOveja => 5;

    /// <summary>True si no cuesta nada desbloquearlo.</summary>
    public bool EstaIncluido => PrecioDesbloqueo == 0;

    public override string ToString()
    {
        return Nombre;
    }
}
