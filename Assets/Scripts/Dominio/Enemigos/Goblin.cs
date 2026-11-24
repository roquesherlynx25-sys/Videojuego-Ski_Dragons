/// <summary>
/// Goblin: corre por la parte baja de la pantalla y salta en diagonal hacia
/// la altura media cuando el jugador se acerca. Enseña a no volar siempre
/// pegado al suelo. Al tocar al jugador le hace daño.
/// </summary>
public class Goblin : Enemigo
{
    public override string Nombre => "Goblin";

    public override EfectoContacto AlTocar(EstadoJugador jugador)
    {
        return EfectoContacto.MataDragon;
    }
}
