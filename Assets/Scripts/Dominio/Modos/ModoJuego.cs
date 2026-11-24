using System;

/// <summary>
/// Clase base abstracta de los modos de juego (Infinito, Niveles, Biomas).
/// Lleva la cuenta del progreso y le dice al juego qué dificultad usar:
/// velocidad, separación de columnas, enemigos y ovejas. Cada modo
/// sobrescribe lo que cambia.
/// </summary>
public abstract class ModoJuego
{
    /// <summary>Velocidad base de las columnas (unidades por segundo).</summary>
    public const float VelocidadBase = 3f;

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

    /// <summary>Distancia entre columnas.</summary>
    public virtual float SeparacionColumnas => 6f;

    /// <summary>True si en este modo aparecen enemigos.</summary>
    public virtual bool HayEnemigos => false;

    /// <summary>Mínimo de columnas entre un enemigo y el siguiente.</summary>
    public virtual int MinColumnasEnemigo => 6;

    /// <summary>Máximo de columnas entre un enemigo y el siguiente.</summary>
    public virtual int MaxColumnasEnemigo => 8;

    /// <summary>Probabilidad (0 a 1) de que el enemigo sea el Grifo; si no, el Goblin.</summary>
    public virtual float ProbabilidadGrifo => 0.5f;

    /// <summary>Multiplicador de la velocidad propia de los enemigos.</summary>
    public virtual float FactorVelocidadEnemigo => 1.1f;

    /// <summary>True si en este modo aparecen ovejas.</summary>
    public virtual bool HayOvejas => false;

    /// <summary>Monedas que da una oveja al explotar.</summary>
    public virtual int RecompensaOveja => 5;

    /// <summary>Factor que alarga la recarga de las habilidades (1 = normal).</summary>
    public virtual float FactorRecarga => 1f;

    /// <summary>Color del cielo del modo.</summary>
    public virtual ColorRgb ColorFondo => new ColorRgb(0.20f, 0.32f, 0.50f);

    /// <summary>Nombre del fondo (se busca como Resources/Sprites/fondo_NOMBRE).</summary>
    public virtual string NombreFondo => "base";

    /// <summary>Velocidad propia de los enemigos (unidades por segundo).</summary>
    public float VelocidadEnemigo => VelocidadBase * FactorVelocidadEnemigo;

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
