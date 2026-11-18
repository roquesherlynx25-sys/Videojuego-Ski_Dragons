using System;

/// <summary>
/// Clase base abstracta de los modos de juego (Infinito, Niveles, Biomas).
/// Lleva la cuenta del progreso; cada modo decide cómo mostrarlo.
/// </summary>
public abstract class ModoJuego
{
    /// <summary>Nombre del modo.</summary>
    public abstract string Nombre { get; }

    /// <summary>Obstáculos superados en la partida.</summary>
    public int Puntos { get; private set; }

    /// <summary>Monedas recogidas en la partida.</summary>
    public int MonedasRecogidas { get; private set; }

    /// <summary>Indica si el modo ya terminó (los finitos lo sobrescriben).</summary>
    public virtual bool HaTerminado => false;

    /// <summary>Evento: cambió el progreso (puntos o monedas).</summary>
    public event Action OnProgresoCambiado;

    /// <summary>Suma un obstáculo superado.</summary>
    public void RegistrarObstaculo()
    {
        Puntos++;
        AlRegistrarObstaculo();
        OnProgresoCambiado?.Invoke();
    }

    /// <summary>Suma monedas recogidas en esta partida.</summary>
    public void RegistrarMoneda(int cantidad)
    {
        if (cantidad <= 0) return;

        MonedasRecogidas += cantidad;
        OnProgresoCambiado?.Invoke();
    }

    /// <summary>Gancho para que cada modo reaccione al superar un obstáculo.</summary>
    protected virtual void AlRegistrarObstaculo()
    {
    }

    /// <summary>Texto de progreso que se muestra en pantalla (polimorfismo).</summary>
    public abstract string TextoProgreso();

    public override string ToString()
    {
        return Nombre + " - " + TextoProgreso();
    }
}
