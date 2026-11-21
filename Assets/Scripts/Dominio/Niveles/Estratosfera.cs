/// <summary>Nivel «Estratosfera» del modo Niveles.</summary>
public class Estratosfera : Nivel
{
    public override string Nombre => "Estratosfera";

    public override int ObstaculosParaCompletar => 14;

    public override ColorRgb ColorFondo => new ColorRgb(0.15f, 0.35f, 0.70f);

    public override float MultiplicadorVelocidad => 1.2f;
}
