/// <summary>
/// Dragón Gorgón [Dureza]: escudo temporal activable y resistencia a los
/// golpes de columna. Tiene 1 vida más un «corazón» extra por cada golpe
/// que resiste su nivel de Dureza (1 golpe a nivel 1, 3 golpes a nivel 5).
/// </summary>
public class DragonGorgon : Dragon
{
    /// <summary>Su habilidad, ya tipada como HabilidadDureza.</summary>
    public HabilidadDureza Dureza { get; }

    /// <summary>Segundos de protección tras resistir un golpe (sube con el nivel).</summary>
    public override float SegundosInvulnerableTrasGolpe => Dureza.InvulnerabilidadTrasGolpe;

    /// <summary>Crea al Gorgón con la habilidad en nivel 1.</summary>
    public DragonGorgon() : this(1)
    {
    }

    /// <summary>Crea al Gorgón con la habilidad en el nivel indicado.</summary>
    public DragonGorgon(int nivelHabilidad) : this(new HabilidadDureza(nivelHabilidad))
    {
    }

    private DragonGorgon(HabilidadDureza habilidad) : base("Gorgón", habilidad, 1 + habilidad.GolpesResistidos)
    {
        Dureza = habilidad;
    }

    public override string Rol => "Dragón de Dureza";
}
