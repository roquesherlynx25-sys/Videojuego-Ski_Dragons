using System;

/// <summary>
/// Clase base abstracta de las habilidades. Maneja nivel, duración y
/// tiempo de recarga (según la TablaHabilidades); cada habilidad
/// concreta define su efecto y su duración.
/// Es C# puro: no usa Unity, así el juego decide cómo aplicar el efecto.
/// </summary>
public abstract class Habilidad : IHabilidad
{
    /// <summary>Nivel máximo que puede alcanzar una habilidad.</summary>
    public const int NivelMaximo = TablaHabilidades.NivelMaximo;

    private int nivel;
    private float duracionRestante;
    private float recargaRestante;

    /// <summary>Nombre de la habilidad.</summary>
    public string Nombre { get; }

    /// <summary>Multiplicador de recarga (lo fija el bioma: 1.0, 1.1, 1.2...).</summary>
    public float MultiplicadorRecarga { get; set; } = 1f;

    /// <summary>Nivel actual (entre 1 y NivelMaximo).</summary>
    public int Nivel
    {
        get => nivel;
        private set => nivel = Math.Clamp(value, TablaHabilidades.NivelMinimo, NivelMaximo);
    }

    /// <summary>Segundos que dura el efecto (cada habilidad lo define).</summary>
    public abstract float Duracion { get; }

    /// <summary>Segundos de recarga: sale de la tabla por nivel, por el factor del bioma.</summary>
    public float RecargaTotal => Math.Max(1f, TablaHabilidades.Recarga(Nivel) * MultiplicadorRecarga);

    /// <summary>Segundos que faltan para poder usarla otra vez.</summary>
    public float RecargaRestante => recargaRestante;

    /// <summary>True mientras el efecto está en marcha.</summary>
    public bool EstaActiva => duracionRestante > 0f;

    /// <summary>True si no está activa y ya terminó su recarga.</summary>
    public bool EstaLista => !EstaActiva && recargaRestante <= 0f;

    /// <summary>Evento: se activó la habilidad.</summary>
    public event Action<Habilidad> OnActivada;

    /// <summary>Evento: terminó la recarga y ya se puede usar.</summary>
    public event Action<Habilidad> OnLista;

    /// <summary>Crea una habilidad de nivel 1.</summary>
    protected Habilidad(string nombre) : this(nombre, TablaHabilidades.NivelMinimo)
    {
    }

    /// <summary>Crea una habilidad con el nivel indicado (sobrecarga).</summary>
    protected Habilidad(string nombre, int nivel)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("La habilidad necesita un nombre.");

        Nombre = nombre;
        Nivel = nivel;
    }

    /// <summary>Activa la habilidad si está lista. Devuelve si lo logró.</summary>
    public bool Activar()
    {
        if (!EstaLista) return false;

        duracionRestante = Duracion;
        AlActivar();
        OnActivada?.Invoke(this);
        return true;
    }

    /// <summary>Avanza el tiempo: cuenta la duración y luego la recarga.</summary>
    public void Actualizar(float deltaTime)
    {
        if (EstaActiva)
        {
            duracionRestante -= deltaTime;
            if (duracionRestante <= 0f)
            {
                duracionRestante = 0f;
                recargaRestante = RecargaTotal; // la recarga empieza al terminar el efecto
                AlTerminar();
            }
        }
        else if (recargaRestante > 0f)
        {
            recargaRestante -= deltaTime;
            if (recargaRestante <= 0f)
            {
                recargaRestante = 0f;
                OnLista?.Invoke(this);
            }
        }
    }

    /// <summary>Sube un nivel (hasta el máximo).</summary>
    public void SubirNivel()
    {
        Nivel = Nivel + 1;
    }

    /// <summary>Gancho: lo que ocurre al activarse (las hijas pueden sobrescribirlo).</summary>
    protected virtual void AlActivar()
    {
    }

    /// <summary>Gancho: lo que ocurre al terminar el efecto.</summary>
    protected virtual void AlTerminar()
    {
    }

    public override string ToString()
    {
        return $"{Nombre} (nivel {Nivel})";
    }
}
