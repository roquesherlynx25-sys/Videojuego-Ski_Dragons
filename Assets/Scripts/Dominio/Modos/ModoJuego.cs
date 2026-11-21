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

    /// <summary>Multiplicador de la velocidad de los obstáculos (1 = normal).</summary>
    public virtual float MultiplicadorVelocidad => 1f;

    /// <summary>Evento: cambió el progreso (puntos, monedas o nivel).</summary>
    public event Action OnProgresoCambiado;

    /// <summary>Suma un obstáculo superado.</summary>
    public void RegistrarObstaculo()
    {
        Puntos++;
        AlRegistrarObstaculo();
        NotificarProgreso();
    }

    /// <summary>Suma monedas recogidas en esta partida.</summary>
    public void RegistrarMoneda(int cantidad)
    {
        if (cantidad <= 0) return;

        MonedasRecogidas += cantidad;
        NotificarProgreso();
    }

    /// <summary>Gancho para que cada modo reaccione al superar un obstáculo.</summary>
    protected virtual void AlRegistrarObstaculo()
    {
    }

    /// <summary>Avisa a quien escuche que el progreso cambió.</summary>
    protected void NotificarProgreso()
    {
        OnProgresoCambiado?.Invoke();
    }

    /// <summary>Texto de progreso que se muestra en pantalla (polimorfismo).</summary>
    public abstract string TextoProgreso();

    public override string ToString()
    {
        return Nombre + " - " + TextoProgreso();
    }
}
