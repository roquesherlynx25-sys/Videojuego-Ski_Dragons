using System;

/// <summary>
/// Clase base abstracta de las runas equipables (se compran en la tienda).
/// Cada runa define su efecto al equiparse.
/// </summary>
public abstract class Runa : ObjetoJuego, IEquipable
{
    /// <summary>Nivel de la runa (1 a 5).</summary>
    public int Nivel { get; }

    protected Runa(string nombre, string descripcion, int precio, int nivel)
        : base(nombre, descripcion, precio, OrigenObjeto.Tienda)
    {
        Nivel = Math.Clamp(nivel, TablaHabilidades.NivelMinimo, TablaHabilidades.NivelMaximo);
    }

    /// <summary>Las runas no se recogen en el mundo: se compran.</summary>
    public override void AlRecoger(EstadoJugador jugador)
    {
    }

    /// <summary>Efecto al equipar la runa (las hijas lo sobrescriben).</summary>
    public virtual void AlEquipar(EstadoJugador jugador)
    {
    }

    /// <summary>Efecto al quitar la runa (las hijas lo sobrescriben).</summary>
    public virtual void AlDesequipar(EstadoJugador jugador)
    {
    }
}
