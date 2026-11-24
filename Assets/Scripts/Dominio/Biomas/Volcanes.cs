/// <summary>
/// Volcanes: hereda de Montañas (herencia entre biomas) y lo vuelve más
/// difícil y más caro. El precio escala ×2 respecto a las Grutas.
/// </summary>
public class Volcanes : Montanas
{
    public override TipoBioma Tipo => TipoBioma.Volcanes;
    public override string Nombre => "Volcanes";
    public override int PrecioDesbloqueo => 1000;
    public override ColorRgb ColorFondo => new ColorRgb(0.35f, 0.10f, 0.08f);
    public override float MultiplicadorVelocidad => 1.5f;
    public override float FactorVelocidadEnemigo => 1.9f;
    public override float SeparacionColumnas => 5.4f;
    public override int MinColumnasEnemigo => 3;
    public override int MaxColumnasEnemigo => 4;
    public override float ProbabilidadGrifo => 0.4f;
    public override float FactorRecarga => 1.25f;
    public override int RecompensaOveja => 25;
}
