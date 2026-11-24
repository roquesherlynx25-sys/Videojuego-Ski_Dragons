/// <summary>Grutas: bioma avanzado. 40% Grifo, 60% Goblin; recarga +20%.</summary>
public class Grutas : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Grutas;
    public override string Nombre => "Grutas";
    public override int PrecioDesbloqueo => 500;
    public override ColorRgb ColorFondo => new ColorRgb(0.18f, 0.15f, 0.22f);
    public override float MultiplicadorVelocidad => 1.4f;
    public override float FactorVelocidadEnemigo => 1.8f;
    public override float SeparacionColumnas => 5.5f;
    public override int MinColumnasEnemigo => 3;
    public override int MaxColumnasEnemigo => 4;
    public override float ProbabilidadGrifo => 0.4f;
    public override float FactorRecarga => 1.2f;
    public override int RecompensaOveja => 18;
}
