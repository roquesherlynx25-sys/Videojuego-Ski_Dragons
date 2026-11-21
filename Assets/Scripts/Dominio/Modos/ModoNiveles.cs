using System;
using System.Collections.Generic;

/// <summary>
/// Modo Niveles (finito): el jugador recorre las capas de la atmósfera,
/// de la Tierra a la Galaxia. Muestra el nombre del nivel y el porcentaje
/// de la etapa. Al completar el último nivel el modo termina.
/// </summary>
public class ModoNiveles : ModoJuego
{
    private readonly List<Nivel> niveles;
    private int indice;
    private int obstaculosEnNivel;
    private bool completado;

    public override string Nombre => "Niveles";

    /// <summary>Nivel en el que se encuentra el jugador.</summary>
    public Nivel NivelActual => niveles[indice];

    /// <summary>Número del nivel actual (empieza en 1).</summary>
    public int NumeroNivel => indice + 1;

    /// <summary>Cuántos niveles tiene el modo.</summary>
    public int TotalNiveles => niveles.Count;

    /// <summary>Porcentaje (0 a 100) de la etapa actual.</summary>
    public int PorcentajeNivel
    {
        get
        {
            if (completado) return 100;
            return (int)(100f * obstaculosEnNivel / NivelActual.ObstaculosParaCompletar);
        }
    }

    /// <summary>Termina cuando se completa el último nivel.</summary>
    public override bool HaTerminado => completado;

    /// <summary>La velocidad de los obstáculos depende del nivel actual.</summary>
    public override float MultiplicadorVelocidad => NivelActual.MultiplicadorVelocidad;

    /// <summary>Evento: el jugador pasó a un nivel nuevo.</summary>
    public event Action<Nivel> OnNivelCambiado;

    /// <summary>Crea el modo con los siete niveles de la atmósfera.</summary>
    public ModoNiveles() : this(CrearNivelesPorDefecto())
    {
    }

    /// <summary>Crea el modo con una lista de niveles propia (sobrecarga).</summary>
    public ModoNiveles(IEnumerable<Nivel> niveles)
    {
        if (niveles == null) throw new ArgumentNullException(nameof(niveles));

        this.niveles = new List<Nivel>(niveles);

        if (this.niveles.Count == 0)
            throw new ArgumentException("Se necesita al menos un nivel.");
    }

    private static List<Nivel> CrearNivelesPorDefecto()
    {
        return new List<Nivel>
        {
            new Tierra(),
            new Troposfera(),
            new Estratosfera(),
            new Mesosfera(),
            new Termosfera(),
            new Exosfera(),
            new Galaxia()
        };
    }

    /// <summary>Cada obstáculo superado avanza la etapa; al llenarla, pasa de nivel.</summary>
    protected override void AlRegistrarObstaculo()
    {
        if (completado) return;

        obstaculosEnNivel++;

        if (obstaculosEnNivel >= NivelActual.ObstaculosParaCompletar)
        {
            AvanzarNivel();
        }
    }

    /// <summary>Completa el nivel actual de golpe (se usa para probar).</summary>
    public void SaltarNivel()
    {
        if (completado) return;

        AvanzarNivel();
        NotificarProgreso();
    }

    private void AvanzarNivel()
    {
        obstaculosEnNivel = 0;

        if (indice >= niveles.Count - 1)
        {
            completado = true;
            return;
        }

        indice++;
        OnNivelCambiado?.Invoke(NivelActual);
    }

    public override string TextoProgreso()
    {
        if (completado)
        {
            return $"¡Todos los niveles completados!   Monedas: {MonedasRecogidas}";
        }

        return $"Nivel {NumeroNivel}/{TotalNiveles}: {NivelActual.Nombre}   Etapa: {PorcentajeNivel}%   Monedas: {MonedasRecogidas}";
    }
}
