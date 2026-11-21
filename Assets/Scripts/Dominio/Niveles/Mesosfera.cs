/// <summary>Nivel «Mesosfera» del modo Niveles.</summary>
public class Mesosfera : Nivel
{
    public override string Nombre => "Mesosfera";

    public override int ObstaculosParaCompletar => 16;

    public override ColorRgb ColorFondo => new ColorRgb(0.12f, 0.22f, 0.50f);

    public override float MultiplicadorVelocidad => 1.3f;
}
