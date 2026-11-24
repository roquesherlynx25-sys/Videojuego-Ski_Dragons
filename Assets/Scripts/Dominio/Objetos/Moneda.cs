/// <summary>Moneda recogible: al tomarla suma monedas al jugador.</summary>
public class Moneda : ObjetoJuego
{
    /// <summary>Cuántas monedas vale esta pieza.</summary>
    public int Valor { get; }

    /// <summary>Crea una moneda de valor 1.</summary>
    public Moneda() : this(1)
    {
    }

    /// <summary>Crea una moneda con el valor indicado (sobrecarga).</summary>
    public Moneda(int valor) : base("Moneda", "Moneda del reino", 0, OrigenObjeto.Biomas)
    {
        Valor = valor < 1 ? 1 : valor;
    }

    /// <summary>Suma el valor (con el multiplicador activo) al jugador.</summary>
    public override void AlRecoger(EstadoJugador jugador)
    {
        jugador.AgregarMonedas(Valor);
    }
}
