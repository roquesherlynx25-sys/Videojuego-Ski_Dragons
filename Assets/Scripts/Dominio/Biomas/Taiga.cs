/// <summary>Bioma «Taiga» (extra): cada bioma posterior es más difícil y más caro.</summary>
public class Taiga : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Taiga;
    public override string Nombre => "Taiga";
    public override int PrecioDesbloqueo => 25000;
    public override ColorRgb ColorFondo => new ColorRgb(0.15f, 0.30f, 0.28f);
    public override float MultiplicadorVelocidad => 1.7f;
    public override float FactorVelocidadEnemigo => 2.3f;
    public override float SeparacionColumnas => 5.2f;
    public override int MinColumnasEnemigo => 2;
    public override int MaxColumnasEnemigo => 3;
    public override float ProbabilidadGrifo => 0.3f;
    public override float FactorRecarga => 1.45f;
    public override int RecompensaOveja => 45;
}
