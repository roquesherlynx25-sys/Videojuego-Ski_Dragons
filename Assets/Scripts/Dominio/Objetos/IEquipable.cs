/// <summary>
/// Contrato de los objetos que el jugador puede equipar
/// desde el menú (Ángel, Oveja, Runas).
/// </summary>
public interface IEquipable
{
    /// <summary>Nombre del objeto.</summary>
    string Nombre { get; }

    /// <summary>Se llama al equiparlo.</summary>
    void AlEquipar(EstadoJugador jugador);

    /// <summary>Se llama al quitarlo.</summary>
    void AlDesequipar(EstadoJugador jugador);
}
