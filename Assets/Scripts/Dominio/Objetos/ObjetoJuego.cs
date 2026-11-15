using System;

/// <summary>
/// Clase base abstracta de todo objeto del juego: monedas, Ángel,
/// Oveja, runas. Cada objeto define qué pasa al recogerlo.
/// </summary>
public abstract class ObjetoJuego
{
    /// <summary>Nombre del objeto.</summary>
    public string Nombre { get; }

    /// <summary>Descripción corta para mostrar en la tienda.</summary>
    public string Descripcion { get; }

    /// <summary>Precio en monedas (0 si no se vende).</summary>
    public int Precio { get; }

    /// <summary>Dónde se puede conseguir el objeto.</summary>
    public OrigenObjeto Origen { get; }

    protected ObjetoJuego(string nombre, string descripcion, int precio, OrigenObjeto origen)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El objeto necesita un nombre.");
        if (precio < 0)
            throw new ArgumentOutOfRangeException(nameof(precio), "El precio no puede ser negativo.");

        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        Origen = origen;
    }

    /// <summary>Indica si se vende en la tienda.</summary>
    public bool EnTienda => (Origen & OrigenObjeto.Tienda) != 0;

    /// <summary>Indica si aparece en los biomas.</summary>
    public bool EnBiomas => (Origen & OrigenObjeto.Biomas) != 0;

    /// <summary>Efecto de recoger el objeto (cada hijo lo define).</summary>
    public abstract void AlRecoger(EstadoJugador jugador);

    public override string ToString()
    {
        return $"{Nombre} ({Origen}) - {Precio} monedas";
    }
}