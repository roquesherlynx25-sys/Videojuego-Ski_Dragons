/// <summary>Bioma «Tundra» (extra): cada bioma posterior es más difícil y más caro.</summary>
public class Tundra : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Tundra;
    public override string Nombre => "Tundra";
    public override int PrecioDesbloqueo => 12500;
    public override ColorRgb ColorFondo => new ColorRgb(0.60f, 0.75f, 0.85f);
    public override float MultiplicadorVelocidad => 1.65f;
    public override float FactorVelocidadEnemigo => 2.2f;
    public override float SeparacionColumnas => 5.2f;
    public override int MinColumnasEnemigo => 2;
    public override int MaxColumnasEnemigo => 3;
    public override float ProbabilidadGrifo => 0.35f;
    public override float FactorRecarga => 1.4f;
    public override int RecompensaOveja => 40;
}
