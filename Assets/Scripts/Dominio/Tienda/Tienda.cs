using System.Collections.Generic;

/// <summary>
/// La tienda del juego (singleton): tiene el catálogo con los precios de
/// la decisión final del equipo y compra artículos con las monedas del jugador.
/// </summary>
public class Tienda
{
    /// <summary>Única instancia de la tienda.</summary>
    public static Tienda Instancia { get; } = new Tienda();

    private readonly List<ArticuloTienda> catalogo = new List<ArticuloTienda>();

    /// <summary>Todo lo que se vende, en el orden en que se muestra.</summary>
    public IReadOnlyList<ArticuloTienda> Catalogo => catalogo;

    // Constructor privado: nadie más puede crear otra tienda.
    private Tienda()
    {
        catalogo.Add(new ArticuloPersonaje(Inventario.NombreAndary, "Dragón Andary", 350,
            "Habilidad de tiempo (x0.5) y quema ovejas."));
        catalogo.Add(new ArticuloPersonaje(Inventario.NombreGorgon, "Dragón Gorgón", 800,
            "Más vidas, escudo y resistencia a columnas; quema ovejas."));

        catalogo.Add(new ArticuloMejora(Inventario.ClaveTiempo, "Mejorar Tiempo (Andary)",
            jugador => jugador.Inventario.TienePersonaje(Inventario.NombreAndary)));
        catalogo.Add(new ArticuloMejora(Inventario.ClaveDureza, "Mejorar Dureza (Gorgón)",
            jugador => jugador.Inventario.TienePersonaje(Inventario.NombreGorgon)));
        catalogo.Add(new ArticuloMejora(Inventario.NombreRunaEscudo, "Mejorar Runa de Escudo",
            jugador => jugador.Inventario.TieneRuna(Inventario.NombreRunaEscudo)));

        catalogo.Add(new ArticuloRuna(Inventario.NombreRunaEscudo, "Runa de Escudo", RunaEscudo.PrecioBase,
            "Escudo temporal que activas con Q."));
        catalogo.Add(new ArticuloRuna(Inventario.NombreRunaImanCodicia, "Runa de Imán / Codicia", RunaImanCodicia.PrecioBase,
            "Atrae monedas y reduce el robo del Grifo a la mitad."));

        catalogo.Add(new ArticuloAngel());
    }

    /// <summary>Compra un artículo con las monedas del jugador.</summary>
    public ResultadoCompra Comprar(ArticuloTienda articulo, EstadoJugador jugador)
    {
        if (articulo == null) return ResultadoCompra.NoDisponible;

        return articulo.Comprar(jugador);
    }
}
