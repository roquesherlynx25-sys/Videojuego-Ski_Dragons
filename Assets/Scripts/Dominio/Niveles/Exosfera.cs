/// <summary>Nivel «Exosfera» del modo Niveles.</summary>
public class Exosfera : Nivel
{
    public override string Nombre => "Exosfera";

    public override int ObstaculosParaCompletar => 20;

    public override ColorRgb ColorFondo => new ColorRgb(0.08f, 0.06f, 0.22f);

    public override float MultiplicadorVelocidad => 1.5f;
}
