/// <summary>
/// Habilidad del Dragón Andary: reduce el tiempo a la mitad (x0.5)
/// mientras está activa. El juego lee EscalaTiempo y la aplica.
/// </summary>
public class HabilidadTiempo : Habilidad
{
    /// <summary>Crea la habilidad en nivel 1.</summary>
    public HabilidadTiempo() : base("Tiempo")
    {
    }

    /// <summary>Crea la habilidad con un nivel dado.</summary>
    public HabilidadTiempo(int nivel) : base("Tiempo", nivel)
    {
    }

    /// <summary>Duración según la tabla (2.5 s a 5 s).</summary>
    public override float Duracion => TablaHabilidades.DuracionTiempo(Nivel);

    /// <summary>Factor de tiempo: 0.5 mientras está activa, 1 normal.</summary>
    public float EscalaTiempo => EstaActiva ? 0.5f : 1f;
}
