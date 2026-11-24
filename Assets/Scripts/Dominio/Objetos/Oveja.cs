/// <summary>
/// Oveja: aparece en los biomas (y en el modo Infinito). Si el jugador
/// lleva un dragón, la quema y tras unos segundos explota como palomita
/// y da monedas; el Jinete no puede quemarla.
/// </summary>
public class Oveja : ObjetoJuego
{
    /// <summary>Monedas que da al explotar.</summary>
    public int Recompensa { get; }

    /// <summary>Crea una oveja con la recompensa base (5 monedas).</summary>
    public Oveja() : this(5)
    {
    }

    /// <summary>Crea una oveja con la recompensa del bioma (sobrecarga).</summary>
    public Oveja(int recompensa) : base("Oveja", "Al quemarla explota como palomita y da monedas.", 0, OrigenObjeto.Biomas)
    {
        Recompensa = recompensa < 1 ? 1 : recompensa;
    }

    /// <summary>Solo los dragones queman ovejas; el Jinete no.</summary>
    public static bool PuedeQuemarla(Personaje personaje)
    {
        return personaje is Dragon;
    }

    /// <summary>Al explotar la oveja suma su recompensa al jugador.</summary>
    public override void AlRecoger(EstadoJugador jugador)
    {
        jugador.AgregarMonedas(Recompensa);
    }
}
