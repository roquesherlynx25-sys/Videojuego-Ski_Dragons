/// <summary>Montañas: bioma intermedio. Mitad Grifo, mitad Goblin; recarga +10%.</summary>
public class Montanas : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Montanas;
    public override string Nombre => "Montañas";
    public override int PrecioDesbloqueo => 250;
    public override ColorRgb ColorFondo => new ColorRgb(0.45f, 0.50f, 0.65f);
    public override float MultiplicadorVelocidad => 1.2f;
    public override float FactorVelocidadEnemigo => 1.44f;
    public override float SeparacionColumnas => 6f;
    public override int MinColumnasEnemigo => 4;
    public override int MaxColumnasEnemigo => 6;
    public override float ProbabilidadGrifo => 0.5f;
    public override float FactorRecarga => 1.1f;
    public override int RecompensaOveja => 10;
}
