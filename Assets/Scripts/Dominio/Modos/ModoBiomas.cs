using System;

/// <summary>
/// Modo Biomas: el jugador elige un bioma desbloqueado y juega sin final.
/// El bioma fija la dificultad (velocidad, enemigos, recarga de habilidades)
/// y las recompensas de las ovejas. Aparecen enemigos según el bioma.
/// </summary>
public class ModoBiomas : ModoJuego
{
    /// <summary>Bioma en el que se juega.</summary>
    public Bioma Bioma { get; }

    public ModoBiomas(Bioma bioma)
    {
        Bioma = bioma ?? throw new ArgumentNullException(nameof(bioma));
    }

    public override string Nombre => "Biomas";

    public override float MultiplicadorVelocidad => Bioma.MultiplicadorVelocidad;
    public override float SeparacionColumnas => Bioma.SeparacionColumnas;
    public override bool HayEnemigos => true;
    public override int MinColumnasEnemigo => Bioma.MinColumnasEnemigo;
    public override int MaxColumnasEnemigo => Bioma.MaxColumnasEnemigo;
    public override float ProbabilidadGrifo => Bioma.ProbabilidadGrifo;
    public override float FactorVelocidadEnemigo => Bioma.FactorVelocidadEnemigo;
    public override bool HayOvejas => true;
    public override int RecompensaOveja => Bioma.RecompensaOveja;
    public override float FactorRecarga => Bioma.FactorRecarga;
    public override ColorRgb ColorFondo => Bioma.ColorFondo;
    public override string NombreFondo => "bioma_" + Bioma.Tipo.ToString().ToLowerInvariant();

    public override string TextoProgreso()
    {
        return $"{Bioma.Nombre}   Puntos: {Puntos}   Monedas: {MonedasRecogidas}";
    }
}
