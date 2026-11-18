/// <summary>
/// Datos que viajan entre escenas (menú → juego). Es una clase estática:
/// existe una sola vez y no hace falta ponerla en ningún objeto.
/// </summary>
public static class SesionJuego
{
    /// <summary>Estado del jugador (monedas, fragmentos, personaje elegido...).</summary>
    public static EstadoJugador Jugador { get; } = new EstadoJugador();

    /// <summary>Modo de juego elegido en el menú.</summary>
    public static TipoModo ModoSeleccionado { get; set; } = TipoModo.Infinito;

    /// <summary>Nombre del usuario (el Jinete lo usa). Luego lo pondrá el login.</summary>
    public static string NombreUsuario { get; set; } = "Jinete";
}