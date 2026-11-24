using System;

/// <summary>
/// Clase base abstracta de todo lo que se vende: personajes, mejoras de
/// habilidad, runas, Ángeles y biomas. Comprar() es un método plantilla:
/// valida, cobra y luego cada artículo aplica su propio efecto.
/// </summary>
public abstract class ArticuloTienda
{
    /// <summary>Nombre que se muestra.</summary>
    public abstract string Nombre { get; }

    /// <summary>Descripción corta del efecto.</summary>
    public abstract string Descripcion { get; }

    /// <summary>Precio actual para este jugador (las mejoras suben de precio).</summary>
    public abstract int Precio(EstadoJugador jugador);

    /// <summary>True si ya no se puede comprar porque ya se tiene.</summary>
    public abstract bool YaAdquirido(EstadoJugador jugador);

    /// <summary>Efecto de la compra (cada artículo lo define).</summary>
    protected abstract void Aplicar(EstadoJugador jugador);

    /// <summary>True si el jugador cumple los requisitos para comprarlo.</summary>
    public virtual bool Disponible(EstadoJugador jugador)
    {
        return true;
    }

    /// <summary>Resultado que se devuelve cuando el artículo ya está adquirido.</summary>
    public virtual ResultadoCompra ResultadoSiAdquirido
    {
        get { return ResultadoCompra.YaAdquirido; }
    }

    /// <summary>Texto extra que depende del jugador (por ejemplo el nivel de la mejora).</summary>
    public virtual string Detalle(EstadoJugador jugador)
    {
        return Descripcion;
    }

    /// <summary>Intenta comprar el artículo.</summary>
    public ResultadoCompra Comprar(EstadoJugador jugador)
    {
        if (jugador == null) throw new ArgumentNullException(nameof(jugador));

        if (!Disponible(jugador)) return ResultadoCompra.NoDisponible;
        if (YaAdquirido(jugador)) return ResultadoSiAdquirido;
        if (!jugador.GastarMonedas(Precio(jugador))) return ResultadoCompra.MonedasInsuficientes;

        Aplicar(jugador);
        return ResultadoCompra.Exito;
    }

    public override string ToString()
    {
        return Nombre;
    }
}
