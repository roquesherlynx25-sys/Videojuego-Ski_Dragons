/// <summary>Nivel «Termosfera» del modo Niveles.</summary>
public class Termosfera : Nivel
{
    public override string Nombre => "Termosfera";

    public override int ObstaculosParaCompletar => 18;

    public override ColorRgb ColorFondo => new ColorRgb(0.20f, 0.12f, 0.40f);

    public override float MultiplicadorVelocidad => 1.4f;
}
