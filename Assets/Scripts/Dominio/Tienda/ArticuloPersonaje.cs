/// <summary>Un personaje que se compra una sola vez (Dragón Andary o Gorgón).</summary>
public class ArticuloPersonaje : ArticuloTienda
{
    private readonly string clave;
    private readonly string nombre;
    private readonly string descripcion;
    private readonly int precio;

    public ArticuloPersonaje(string clave, string nombre, int precio, string descripcion)
    {
        this.clave = clave;
        this.nombre = nombre;
        this.precio = precio;
        this.descripcion = descripcion;
    }

    public override string Nombre => nombre;
    public override string Descripcion => descripcion;

    public override int Precio(EstadoJugador jugador) { return precio; }

    public override bool YaAdquirido(EstadoJugador jugador)
    {
        return jugador.Inventario.TienePersonaje(clave);
    }

    protected override void Aplicar(EstadoJugador jugador)
    {
        jugador.Inventario.AgregarPersonaje(clave);
    }
}
