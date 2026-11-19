using System;
using System.Collections.Generic;

/// <summary>
/// Guarda todo lo que tiene el jugador: monedas, fragmentos de Ángel,
/// objetos equipados y el personaje elegido.
/// </summary>
public class EstadoJugador
{
    private int monedas;
    private int fragmentosAngel;
    private readonly List<IEquipable> equipados = new List<IEquipable>();

    /// <summary>Monedas disponibles (solo se cambian con los métodos).</summary>
    public int Monedas => monedas;

    /// <summary>Fragmentos de Ángel reunidos.</summary>
    public int FragmentosAngel => fragmentosAngel;

    /// <summary>Multiplicador de monedas (una runa lo pondrá en 2).</summary>
    public int MultiplicadorMonedas { get; set; } = 1;

    /// <summary>Personaje con el que se va a jugar.</summary>
    public Personaje PersonajeSeleccionado { get; set; }

    /// <summary>Objetos equipados, en solo lectura.</summary>
    public IReadOnlyList<IEquipable> Equipados => equipados;

    /// <summary>Evento: cambió la cantidad de monedas.</summary>
    public event Action<int> OnMonedasCambiadas;

    /// <summary>Evento: cambió la cantidad de fragmentos de Ángel.</summary>
    public event Action<int> OnFragmentosCambiados;

    /// <summary>Suma monedas aplicando el multiplicador activo.</summary>
    public void AgregarMonedas(int cantidad)
    {
        if (cantidad <= 0) return;

        monedas += cantidad * MultiplicadorMonedas;
        OnMonedasCambiadas?.Invoke(monedas);
    }

    /// <summary>Gasta monedas. Devuelve false si no alcanzan.</summary>
    public bool GastarMonedas(int cantidad)
    {
        if (cantidad < 0 || cantidad > monedas) return false;

        monedas -= cantidad;
        OnMonedasCambiadas?.Invoke(monedas);
        return true;
    }

    /// <summary>Suma un fragmento de Ángel.</summary>
    public void AgregarFragmentoAngel()
    {
        fragmentosAngel++;
        OnFragmentosCambiados?.Invoke(fragmentosAngel);
    }

    /// <summary>Gasta fragmentos de Ángel. Devuelve false si no alcanzan.</summary>
    public bool GastarFragmentosAngel(int cantidad)
    {
        if (cantidad < 0 || cantidad > fragmentosAngel) return false;

        fragmentosAngel -= cantidad;
        OnFragmentosCambiados?.Invoke(fragmentosAngel);
        return true;
    }

    /// <summary>Carga los valores guardados (monedas y fragmentos).</summary>
    public void Cargar(int monedasGuardadas, int fragmentosGuardados)
    {
        monedas = Math.Max(0, monedasGuardadas);
        fragmentosAngel = Math.Max(0, fragmentosGuardados);
        OnMonedasCambiadas?.Invoke(monedas);
        OnFragmentosCambiados?.Invoke(fragmentosAngel);
    }

    /// <summary>Equipa un objeto si no lo tenía ya. Devuelve si lo logró.</summary>
    public bool Equipar(IEquipable objeto)
    {
        if (objeto == null || equipados.Contains(objeto)) return false;

        equipados.Add(objeto);
        objeto.AlEquipar(this);
        return true;
    }

    /// <summary>Quita un objeto equipado. Devuelve si lo logró.</summary>
    public bool Desequipar(IEquipable objeto)
    {
        if (objeto == null || !equipados.Remove(objeto)) return false;

        objeto.AlDesequipar(this);
        return true;
    }

    /// <summary>
    /// Busca entre los equipados un objeto de un tipo concreto
    /// (por ejemplo Angel). Devuelve null si no hay ninguno.
    /// </summary>
    public T ObtenerEquipado<T>() where T : class, IEquipable
    {
        foreach (IEquipable objeto in equipados)
        {
            if (objeto is T encontrado) return encontrado;
        }
        return null;
    }

    public override string ToString()
    {
        string personaje = PersonajeSeleccionado != null ? PersonajeSeleccionado.Nombre : "ninguno";
        return $"Monedas: {monedas} | Fragmentos: {fragmentosAngel} | Equipados: {equipados.Count} | Personaje: {personaje}";
    }
}
