using System;

/// <summary>
/// Clase base abstracta de todo personaje jugable (jinete o dragón).
/// No se puede instanciar directamente: cada tipo de personaje
/// la hereda y define su propio rol y habilidad.
/// </summary>
public abstract class Personaje
{
    // Campos privados: solo la propia clase los modifica (encapsulamiento).
    private string nombre;
    private int vidas;

    /// <summary>Nombre del personaje. No puede estar vacío.</summary>
    public string Nombre
    {
        get { return nombre; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre del personaje no puede estar vacío.");
            nombre = value.Trim();
        }
    }

    /// <summary>Vidas actuales (solo lectura desde fuera).</summary>
    public int Vidas => vidas;

    /// <summary>Vidas con las que empieza el personaje.</summary>
    public int VidasMaximas { get; }

    /// <summary>Indica si todavía le quedan vidas.</summary>
    public bool EstaVivo => vidas > 0;

    /// <summary>Rol del personaje (cada clase hija lo define).</summary>
    public abstract string Rol { get; }

    /// <summary>Por defecto un personaje no tiene habilidad.</summary>
    public virtual bool TieneHabilidad => false;

    /// <summary>Evento: avisa cuando el personaje pierde su última vida.</summary>
    public event Action<Personaje> OnMuerte;

    /// <summary>Constructor con una vida por defecto.</summary>
    protected Personaje(string nombre) : this(nombre, 1)
    {
    }

    /// <summary>Constructor con vidas personalizadas (sobrecarga).</summary>
    protected Personaje(string nombre, int vidas)
    {
        if (vidas < 1)
            throw new ArgumentOutOfRangeException(nameof(vidas), "Debe tener al menos 1 vida.");

        Nombre = nombre;
        VidasMaximas = vidas;
        this.vidas = vidas;
    }

    /// <summary>Recibe un golpe de 1 de daño (sobrecarga sin parámetros).</summary>
    public void RecibirGolpe()
    {
        RecibirGolpe(1);
    }

    /// <summary>Recibe un golpe con el daño indicado (sobrecarga con parámetro).</summary>
    public void RecibirGolpe(int dano)
    {
        if (!EstaVivo || dano <= 0) return;

        vidas = Math.Max(0, vidas - dano);

        if (vidas == 0)
        {
            OnMuerte?.Invoke(this);
        }
    }

    /// <summary>Vuelve a la vida con 1 vida (lo usa el Ángel al revivir).</summary>
    public void Revivir()
    {
        if (vidas <= 0)
        {
            vidas = 1;
        }
    }

    /// <summary>
    /// Activa la habilidad del personaje. Por defecto no hace nada;
    /// los dragones lo sobrescriben (polimorfismo).
    /// </summary>
    public virtual void UsarHabilidad()
    {
    }

    /// <summary>Texto con los datos principales del personaje.</summary>
    public override string ToString()
    {
        return $"{Nombre} ({Rol}) - Vidas: {vidas}/{VidasMaximas}";
    }
}
