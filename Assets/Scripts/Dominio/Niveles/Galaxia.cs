/// <summary>Nivel «Galaxia» del modo Niveles.</summary>
public class Galaxia : Nivel
{
    public override string Nombre => "Galaxia";

    public override int ObstaculosParaCompletar => 25;

    public override ColorRgb ColorFondo => new ColorRgb(0.03f, 0.02f, 0.10f);

    public override float MultiplicadorVelocidad => 1.6f;
}
