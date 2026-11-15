/// <summary>
/// Habilidad del Dragón Gorgón: un escudo breve durante el cual los
/// obstáculos no le hacen nada. Más nivel = escudo más largo.
/// </summary>
public class HabilidadDureza : Habilidad
{
    private const float RecargaInicial = 15f;

    /// <summary>Crea la habilidad en nivel 1.</summary>
    public HabilidadDureza() : base("Dureza", RecargaInicial)
    {
    }

    /// <summary>Crea la habilidad con un nivel dado.</summary>
    public HabilidadDureza(int nivel) : base("Dureza", RecargaInicial, nivel)
    {
    }

    /// <summary>Dura 2 s y un segundo más por nivel.</summary>
    public override float Duracion => 2f + (Nivel - 1);

    /// <summary>True mientras el escudo está activo.</summary>
    public bool EsInmune => EstaActiva;
}