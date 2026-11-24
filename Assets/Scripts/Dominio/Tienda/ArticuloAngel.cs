/// <summary>
/// Ángel de reserva (consumible): si se equipa, la partida empieza con 1 de
/// 3 fragmentos. Se puede comprar varias veces; se usa uno por partida.
/// </summary>
public class ArticuloAngel : ArticuloTienda
{
    /// <summary>Precio en monedas.</summary>
    public const int PrecioAngel = 200;

    public override string Nombre => "Ángel";
    public override string Descripcion => "Empiezas la partida con 1 de 3 fragmentos (máximo 1 por partida).";

    public override int Precio(EstadoJugador jugador) { return PrecioAngel; }

    public override bool YaAdquirido(EstadoJugador jugador)
    {
        return jugador.Inventario.AngelesConsumibles >= Inventario.MaximoAngeles;
    }

    public override string Detalle(EstadoJugador jugador)
    {
        return Descripcion + " Tienes " + jugador.Inventario.AngelesConsumibles + ".";
    }

    protected override void Aplicar(EstadoJugador jugador)
    {
        jugador.Inventario.AgregarAngel();
    }
}
