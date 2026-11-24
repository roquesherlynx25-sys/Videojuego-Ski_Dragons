/// <summary>
/// Runa de Escudo: otorga un escudo temporal que el jugador activa con la
/// tecla Q. Usa la misma habilidad de Dureza (composición), con su propia
/// recarga y su propio nivel.
/// </summary>
public class RunaEscudo : Runa
{
    /// <summary>Precio en la tienda.</summary>
    public const int PrecioBase = 450;

    /// <summary>Escudo que activa la runa.</summary>
    public HabilidadDureza Escudo { get; }

    /// <summary>Crea la runa en nivel 1.</summary>
    public RunaEscudo() : this(1)
    {
    }

    /// <summary>Crea la runa con el nivel indicado (sobrecarga).</summary>
    public RunaEscudo(int nivel) : base("Runa de Escudo", "Escudo temporal que activas con la tecla Q.", PrecioBase, nivel)
    {
        Escudo = new HabilidadDureza(Nivel);
    }
}
