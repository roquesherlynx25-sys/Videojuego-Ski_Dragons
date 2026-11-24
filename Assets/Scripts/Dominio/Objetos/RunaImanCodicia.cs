/// <summary>
/// Runa de Imán / Codicia: atrae las monedas cercanas y reduce a la mitad
/// lo que roba el Grifo.
/// </summary>
public class RunaImanCodicia : Runa
{
    /// <summary>Precio en la tienda.</summary>
    public const int PrecioBase = 700;

    /// <summary>Distancia a la que atrae las monedas.</summary>
    public const float RadioIman = 4f;

    /// <summary>Fracción de lo robado que se queda el Grifo (50%).</summary>
    public const float FactorRobo = 0.5f;

    public RunaImanCodicia() : base("Runa de Imán / Codicia", "Atrae monedas y reduce el robo del Grifo a la mitad.", PrecioBase, 1)
    {
    }

    public override void AlEquipar(EstadoJugador jugador)
    {
        jugador.FactorRobo = FactorRobo;
    }

    public override void AlDesequipar(EstadoJugador jugador)
    {
        jugador.FactorRobo = 1f;
    }
}
