/// <summary>
/// Habilidad del Dragón Gorgón (y de la Runa de Escudo): un escudo breve
/// durante el cual los obstáculos no le hacen nada. Más nivel = escudo
/// más largo, más golpes de columna resistidos y más protección tras el golpe.
/// </summary>
public class HabilidadDureza : Habilidad
{
    /// <summary>Crea la habilidad en nivel 1.</summary>
    public HabilidadDureza() : base("Dureza")
    {
    }

    /// <summary>Crea la habilidad con un nivel dado.</summary>
    public HabilidadDureza(int nivel) : base("Dureza", nivel)
    {
    }

    /// <summary>Duración del escudo según la tabla (3 s a 7 s).</summary>
    public override float Duracion => TablaHabilidades.DuracionEscudo(Nivel);

    /// <summary>True mientras el escudo está activo.</summary>
    public bool EsInmune => EstaActiva;

    /// <summary>Golpes de columna extra que resiste el dragón (1 a 3).</summary>
    public int GolpesResistidos => TablaHabilidades.GolpesResistidos(Nivel);

    /// <summary>Segundos de protección tras resistir un golpe.</summary>
    public float InvulnerabilidadTrasGolpe => TablaHabilidades.InvulnerabilidadTrasGolpe(Nivel);
}
