using System;

/// <summary>
/// Mejora de una habilidad (Tiempo, Dureza o Runa de Escudo). Sube un nivel
/// cada vez que se compra; el precio sale de la tabla de habilidades y solo
/// se puede comprar si el jugador ya tiene el personaje o la runa.
/// </summary>
public class ArticuloMejora : ArticuloTienda
{
    private readonly string claveHabilidad;
    private readonly string nombre;
    private readonly Func<EstadoJugador, bool> requisito;

    public ArticuloMejora(string claveHabilidad, string nombre, Func<EstadoJugador, bool> requisito)
    {
        this.claveHabilidad = claveHabilidad;
        this.nombre = nombre;
        this.requisito = requisito;
    }

    public override string Nombre => nombre;
    public override string Descripcion => "Sube la habilidad al siguiente nivel (máximo 5).";

    public override ResultadoCompra ResultadoSiAdquirido
    {
        get { return ResultadoCompra.NivelMaximo; }
    }

    private int NivelActual(EstadoJugador jugador)
    {
        return jugador.Inventario.NivelHabilidad(claveHabilidad);
    }

    public override bool Disponible(EstadoJugador jugador)
    {
        return requisito == null || requisito(jugador);
    }

    public override bool YaAdquirido(EstadoJugador jugador)
    {
        return NivelActual(jugador) >= TablaHabilidades.NivelMaximo;
    }

    public override int Precio(EstadoJugador jugador)
    {
        int nivel = NivelActual(jugador);
        if (nivel >= TablaHabilidades.NivelMaximo) return 0;
        return TablaHabilidades.PrecioDeNivel(nivel + 1);
    }

    public override string Detalle(EstadoJugador jugador)
    {
        int nivel = NivelActual(jugador);
        if (nivel >= TablaHabilidades.NivelMaximo) return "Nivel máximo (5)";
        return "Nivel " + nivel + " a " + (nivel + 1);
    }

    protected override void Aplicar(EstadoJugador jugador)
    {
        jugador.Inventario.SubirNivelHabilidad(claveHabilidad);
    }
}
