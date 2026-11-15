using System;

/// <summary>
/// Clase base abstracta de los dragones. Hereda de Personaje y agrega
/// una habilidad (composición: el dragón TIENE una Habilidad).
/// </summary>
public abstract class Dragon : Personaje
{
    /// <summary>Habilidad propia del dragón.</summary>
    public Habilidad Habilidad { get; }

    protected Dragon(string nombre, Habilidad habilidad) : base(nombre)
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