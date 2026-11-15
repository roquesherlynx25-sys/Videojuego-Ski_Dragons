/// <summary>
/// Habilidad del Dragón Andary: reduce el tiempo a la mitad (x0.5)
/// mientras está activa. El juego lee EscalaTiempo y la aplica.
/// </summary>
public class HabilidadTiempo : Habilidad
{
    private const float RecargaInicial = 12f;

    /// <summary>Crea la habilidad en nivel 1.</summary>
    public HabilidadTiempo() : base("Tiempo", RecargaInicial)
    {
    }

    /// <summary>Crea la habilidad con un nivel dado.</summary>
    public HabilidadTiempo(int nivel) : base("Tiempo", RecargaInicial, nivel)
    {
    }

    /// <summary>Dura 3 s y medio segundo más por nivel.</summary>
    public override float Duracion => 3f + 0.5f * (Nivel - 1);

    /// <summary>Factor de tiempo: 0.5 mientras está activa, 1 normal.</summary>
    public float EscalaTiempo => EstaActiva ? 0.5f : 1f;
}