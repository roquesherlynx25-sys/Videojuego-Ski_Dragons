using System;
using System.Collections.Generic;

/// <summary>
/// Guarda todo lo que tiene el jugador: monedas, fragmentos de Ángel de la
/// partida, objetos equipados, el personaje elegido y su inventario
/// (lo comprado en la tienda).
/// </summary>
public class EstadoJugador
{
    private int monedas;
    private int fragmentosAngel;
    private readonly List<IEquipable> equipados = new List<IEquipable>();

    /// <summary>Monedas disponibles (solo se cambian con los métodos).</summary>
    public int Monedas => monedas;

    /// <summary>Fragmentos de Ángel reunidos en la partida actual.</summary>
    public int FragmentosAngel => fragmentosAngel;

    /// <summary>Multiplicador de monedas.</summary>
    public int MultiplicadorMonedas { get; set; } = 1;

    /// <summary>Fracción de lo que roba el Grifo (1 = todo; la Runa de Codicia lo baja a 0.5).</summary>
    public float FactorRobo { get; set; } = 1f;

    /// <summary>Personaje con el que se va a jugar.</summary>
    public Personaje PersonajeSeleccionado { get; set; }

    /// <summary>Lo comprado en la tienda (personajes, biomas, niveles, runas, Ángeles).</summary>
    public Inventario Inventario { get; private set; } = new Inventario();

    /// <summary>Objetos equipados en la partida, en solo lectura.</summary>
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

    /// <summary>Pierde monedas (por ejemplo, un robo). Devuelve cuántas perdió de verdad.</summary>
    public int PerderMonedas(int cantidad)
    {
        int perdidas = Math.Min(Math.Max(cantidad, 0), monedas);
        monedas -= perdidas;
        OnMonedasCambiadas?.Invoke(monedas);
        return perdidas;
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

    /// <summary>Reemplaza el inventario (al cargar el progreso guardado).</summary>
    public void CargarInventario(Inventario inventario)
    {
        Inventario = inventario ?? new Inventario();
    }

    /// <summary>
    /// Se llama al empezar cada partida: reinicia los fragmentos, vuelve a
    /// equipar las runas elegidas y, si el Ángel está equipado y hay uno de
    /// reserva, empieza con 1 de 3 fragmentos.
    /// </summary>
    public void PrepararPartida()
    {
        foreach (IEquipable objeto in new List<IEquipable>(equipados))
        {
            Desequipar(objeto);
        }

        FactorRobo = 1f;
        fragmentosAngel = 0;

        if (Inventario.TieneRuna(Inventario.NombreRunaEscudo) && Inventario.EstaEquipada(Inventario.NombreRunaEscudo))
        {
            Equipar(new RunaEscudo(Inventario.NivelHabilidad(Inventario.NombreRunaEscudo)));
        }

        if (Inventario.TieneRuna(Inventario.NombreRunaImanCodicia) && Inventario.EstaEquipada(Inventario.NombreRunaImanCodicia))
        {
            Equipar(new RunaImanCodicia());
        }

        if (Inventario.EstaEquipada(Inventario.NombreAngel) && Inventario.UsarAngel())
        {
            fragmentosAngel = 1;
        }

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
    /// (por ejemplo RunaEscudo). Devuelve null si no hay ninguno.
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
