/// <summary>Una runa equipable que se compra una sola vez.</summary>
public class ArticuloRuna : ArticuloTienda
{
    private readonly string clave;
    private readonly string nombre;
    private readonly string descripcion;
    private readonly int precio;

    public ArticuloRuna(string clave, string nombre, int precio, string descripcion)
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
        return jugador.Inventario.TieneRuna(clave);
    }

    protected override void Aplicar(EstadoJugador jugador)
    {
        jugador.Inventario.AgregarRuna(clave);
    }
}
