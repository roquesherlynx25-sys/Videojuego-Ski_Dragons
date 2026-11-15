/// <summary>Dragón Gorgón: su habilidad es un escudo temporal [Dureza].</summary>
public class DragonGorgon : Dragon
{
    /// <summary>Su habilidad, ya tipada como HabilidadDureza.</summary>
    public HabilidadDureza Dureza { get; }

    /// <summary>Crea al Gorgón con la habilidad en nivel 1.</summary>
    public DragonGorgon() : this(1)
    {
    }

    /// <summary>Crea al Gorgón con la habilidad en el nivel indicado.</summary>
    public DragonGorgon(int nivelHabilidad) : this(new HabilidadDureza(nivelHabilidad))
    {
    }

    private DragonGorgon(HabilidadDureza habilidad) : base("Gorgón", habilidad)
    {
        Dureza = habilidad;
    }

    public override string Rol => "Dragón de Dureza";
}