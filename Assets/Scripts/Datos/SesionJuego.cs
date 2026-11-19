/// <summary>
/// Datos que viajan entre escenas (login → menú → juego). Es una clase
/// estática: existe una sola vez y no hace falta ponerla en ningún objeto.
/// </summary>
public static class SesionJuego
{
    /// <summary>Estado del jugador (monedas, fragmentos, personaje elegido...).</summary>
    public static EstadoJugador Jugador { get; } = CrearJugador();

    /// <summary>Modo de juego elegido en el menú.</summary>
    public static TipoModo ModoSeleccionado { get; set; } = TipoModo.Infinito;

    /// <summary>Nombre del usuario (el Jinete lo usa). Lo pone el login.</summary>
    public static string NombreUsuario { get; set; } = "Jinete";

    /// <summary>Crea al jugador y le carga el progreso guardado.</summary>
    private static EstadoJugador CrearJugador()
    {
        EstadoJugador jugador = new EstadoJugador();
        Persistencia.Cargar(jugador);
        return jugador;
    }
}
