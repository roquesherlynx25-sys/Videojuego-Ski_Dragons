/// <summary>Nivel «Troposfera» del modo Niveles.</summary>
public class Troposfera : Nivel
{
    public override string Nombre => "Troposfera";

    public override int ObstaculosParaCompletar => 12;

    public override ColorRgb ColorFondo => new ColorRgb(0.25f, 0.55f, 0.90f);

    public override float MultiplicadorVelocidad => 1.1f;
}
