/// <summary>Bioma «Marino» (extra): cada bioma posterior es más difícil y más caro.</summary>
public class Marino : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Marino;
    public override string Nombre => "Marino";
    public override int PrecioDesbloqueo => 50000;
    public override ColorRgb ColorFondo => new ColorRgb(0.05f, 0.25f, 0.50f);
    public override float MultiplicadorVelocidad => 1.75f;
    public override float FactorVelocidadEnemigo => 2.4f;
    public override float SeparacionColumnas => 5.2f;
    public override int MinColumnasEnemigo => 2;
    public override int MaxColumnasEnemigo => 3;
    public override float ProbabilidadGrifo => 0.3f;
    public override float FactorRecarga => 1.5f;
    public override int RecompensaOveja => 50;
}
