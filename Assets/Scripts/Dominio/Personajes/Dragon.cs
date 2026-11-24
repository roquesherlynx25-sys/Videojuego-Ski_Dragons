using System;

/// <summary>
/// Clase base abstracta de los dragones. Hereda de Personaje y agrega
/// una habilidad (composición: el dragón TIENE una Habilidad).
/// </summary>
public abstract class Dragon : Personaje
{
    /// <summary>Habilidad propia del dragón.</summary>
    public Habilidad Habilidad { get; }

    /// <summary>Segundos de protección después de resistir un golpe.</summary>
    public virtual float SegundosInvulnerableTrasGolpe => 0.5f;

    /// <summary>Crea un dragón con una vida.</summary>
    protected Dragon(string nombre, Habilidad habilidad) : this(nombre, habilidad, 1)
    {
    }

    /// <summary>Crea un dragón con las vidas indicadas (sobrecarga).</summary>
    protected Dragon(string nombre, Habilidad habilidad, int vidas) : base(nombre, vidas)
    {
        Habilidad = habilidad ?? throw new ArgumentNullException(nameof(habilidad));
    }

    /// <summary>Todo dragón tiene habilidad (sobrescribe a Personaje).</summary>
    public override bool TieneHabilidad => true;

    /// <summary>Activa la habilidad del dragón (polimorfismo).</summary>
    public override void UsarHabilidad()
    {
        Habilidad.Activar();
    }

    /// <summary>Hace avanzar el tiempo de la habilidad.</summary>
    public void Actualizar(float deltaTime)
    {
        Habilidad.Actualizar(deltaTime);
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Habilidad: {Habilidad}";
    }
}
