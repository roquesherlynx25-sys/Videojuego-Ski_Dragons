using System;

/// <summary>
/// Ángel: objeto equipable que funciona como un tótem. Con 3 fragmentos
/// revive al jugador cuando muere; si falta un fragmento, el jugador
/// puede responder una pregunta simple de POO para completarlo.
/// Se consigue en la tienda y en los biomas (T-B).
/// </summary>
public class Angel : ObjetoJuego, IEquipable
{
    /// <summary>Fragmentos necesarios para formar el tótem.</summary>
    public const int FragmentosNecesarios = 3;

    // Precio provisional hasta definir la tabla de precios.
    private const int PrecioInicial = 100;

    /// <summary>Crea el Ángel (disponible en tienda y biomas).</summary>
    public Angel() : base(
        "Ángel",
        "Con 3 fragmentos te revive. Con 2, responde una pregunta de POO.",
        PrecioInicial,
        OrigenObjeto.Ambos)
    {
    }

    /// <summary>True si el jugador ya tiene los 3 fragmentos.</summary>
    public bool PuedeRevivir(EstadoJugador jugador)
    {
        Validar(jugador);
        return jugador.FragmentosAngel >= FragmentosNecesarios;
    }

    /// <summary>True si falta exactamente un fragmento (se puede ganar con la pregunta).</summary>
    public bool PuedeRevivirConPregunta(EstadoJugador jugador)
    {
        Validar(jugador);
        return jugador.FragmentosAngel == FragmentosNecesarios - 1;
    }

    /// <summary>Gasta los fragmentos al revivir. Devuelve false si no alcanzan.</summary>
    public bool Consumir(EstadoJugador jugador)
    {
        Validar(jugador);
        return jugador.GastarFragmentosAngel(FragmentosNecesarios);
    }

    /// <summary>Recoger el Ángel en un bioma da un fragmento.</summary>
    public override void AlRecoger(EstadoJugador jugador)
    {
        Validar(jugador);
        jugador.AgregarFragmentoAngel();
    }

    /// <summary>El Ángel no necesita preparar nada al equiparse.</summary>
    public void AlEquipar(EstadoJugador jugador)
    {
    }

    /// <summary>El Ángel no necesita limpiar nada al quitarse.</summary>
    public void AlDesequipar(EstadoJugador jugador)
    {
    }

    private static void Validar(EstadoJugador jugador)
    {
        if (jugador == null) throw new ArgumentNullException(nameof(jugador));
    }
}
