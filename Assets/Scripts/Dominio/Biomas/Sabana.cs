/// <summary>Sabana: bioma inicial y fácil (incluido gratis). Solo aparece el Grifo.</summary>
public class Sabana : Bioma
{
    public override TipoBioma Tipo => TipoBioma.Sabana;
    public override string Nombre => "Sabana";
    public override int PrecioDesbloqueo => 0;
    public override ColorRgb ColorFondo => new ColorRgb(0.80f, 0.65f, 0.35f);
}
