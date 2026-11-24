/// <summary>Bioma «Selva» (extra): cada bioma posterior es más difícil y más caro.</summary>
public class Selva : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Selva;
    public override string Nombre => "Selva";
    public override int PrecioDesbloqueo => 2500;
    public override ColorRgb ColorFondo => new ColorRgb(0.10f, 0.38f, 0.22f);
    public override float MultiplicadorVelocidad => 1.55f;
    public override float FactorVelocidadEnemigo => 2.0f;
    public override float SeparacionColumnas => 5.3f;
    public override int MinColumnasEnemigo => 2;
    public override int MaxColumnasEnemigo => 3;
    public override float ProbabilidadGrifo => 0.4f;
    public override float FactorRecarga => 1.3f;
    public override int RecompensaOveja => 30;
}
