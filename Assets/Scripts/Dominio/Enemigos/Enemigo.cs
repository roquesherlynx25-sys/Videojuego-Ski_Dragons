/// <summary>
/// Clase base abstracta de los enemigos. Cada enemigo decide qué le hace
/// al jugador cuando lo toca (polimorfismo).
/// </summary>
public abstract class Enemigo
{
    /// <summary>Nombre del enemigo.</summary>
    public abstract string Nombre { get; }

    /// <summary>Efecto sobre el jugador al tocarlo.</summary>
    public abstract EfectoContacto AlTocar(EstadoJugador jugador);

    public override string ToString()
    {
        return Nombre;
    }
}
