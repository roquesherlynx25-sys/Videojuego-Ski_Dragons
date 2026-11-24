/// <summary>Bioma «Desierto» (extra): cada bioma posterior es más difícil y más caro.</summary>
public class Desierto : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Desierto;
    public override string Nombre => "Desierto";
    public override int PrecioDesbloqueo => 5000;
    public override ColorRgb ColorFondo => new ColorRgb(0.78f, 0.55f, 0.30f);
    public override float MultiplicadorVelocidad => 1.6f;
    public override float FactorVelocidadEnemigo => 2.1f;
    public override float SeparacionColumnas => 5.2f;
    public override int MinColumnasEnemigo => 2;
    public override int MaxColumnasEnemigo => 3;
    public override float ProbabilidadGrifo => 0.35f;
    public override float FactorRecarga => 1.35f;
    public override int RecompensaOveja => 35;
}
