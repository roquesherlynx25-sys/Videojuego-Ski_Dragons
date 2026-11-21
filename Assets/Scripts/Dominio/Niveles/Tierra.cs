/// <summary>Nivel «Tierra» del modo Niveles.</summary>
public class Tierra : Nivel
{
    public override string Nombre => "Tierra";

    public override int ObstaculosParaCompletar => 10;

    public override ColorRgb ColorFondo => new ColorRgb(0.35f, 0.60f, 0.80f);

    public override float MultiplicadorVelocidad => 1.0f;
}
