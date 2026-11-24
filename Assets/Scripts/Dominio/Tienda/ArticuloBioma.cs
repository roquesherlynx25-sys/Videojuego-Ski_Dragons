using System;

/// <summary>Desbloqueo de un bioma con monedas (se compra una sola vez).</summary>
public class ArticuloBioma : ArticuloTienda
{
    /// <summary>Bioma que se desbloquea.</summary>
    public Bioma Bioma { get; }

    public ArticuloBioma(Bioma bioma)
    {
        Bioma = bioma ?? throw new ArgumentNullException(nameof(bioma));
    }

    public override string Nombre => "Bioma " + Bioma.Nombre;
    public override string Descripcion => "Desbloquea el bioma " + Bioma.Nombre + ".";

    public override int Precio(EstadoJugador jugador) { return Bioma.PrecioDesbloqueo; }

    public override bool YaAdquirido(EstadoJugador jugador)
    {
        return jugador.Inventario.TieneBioma(Bioma.Tipo);
    }

    protected override void Aplicar(EstadoJugador jugador)
    {
        jugador.Inventario.DesbloquearBioma(Bioma.Tipo);
    }
}
