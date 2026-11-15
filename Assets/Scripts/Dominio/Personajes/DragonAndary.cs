/// <summary>Dragón Andary: su habilidad es manipular el tiempo [Tiempo].</summary>
public class DragonAndary : Dragon
{
    /// <summary>Su habilidad, ya tipada como HabilidadTiempo.</summary>
    public HabilidadTiempo Tiempo { get; }

    /// <summary>Crea a Andary con la habilidad en nivel 1.</summary>
    public DragonAndary() : this(1)
    {
    }

    /// <summary>Crea a Andary con la habilidad en el nivel indicado.</summary>
    public DragonAndary(int nivelHabilidad) : this(new HabilidadTiempo(nivelHabilidad))
    {
    }

    private DragonAndary(HabilidadTiempo habilidad) : base("Andary", habilidad)
    {
        Tiempo = habilidad;
    }

    public override string Rol => "Dragón del Tiempo";
}