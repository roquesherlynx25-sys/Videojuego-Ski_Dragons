using System;

/// <summary>
/// Grifo Kivalair: enemigo ladrón que aparece en todos los biomas. Al
/// tocar al jugador le roba 15 monedas (la mitad con la Runa de Codicia).
/// </summary>
public class Grifo : Enemigo
{
    /// <summary>Monedas que roba normalmente.</summary>
    public const int RoboBase = 15;

    /// <summary>Monedas que robó en el último contacto.</summary>
    public int UltimoRobo { get; private set; }

    public override string Nombre => "Grifo Kivalair";

    public override EfectoContacto AlTocar(EstadoJugador jugador)
    {
        if (jugador == null) throw new ArgumentNullException(nameof(jugador));

        int robo = (int)Math.Round(RoboBase * jugador.FactorRobo);
        UltimoRobo = jugador.PerderMonedas(robo);
        return EfectoContacto.RoboMonedas;
    }
}
