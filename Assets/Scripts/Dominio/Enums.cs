using System;

/// <summary>
/// Estados posibles de una partida. El GameManager cambia entre ellos.
/// </summary>
public enum EstadoPartida
{
    Jugando,
    Pregunta,
    GameOver,
    Victoria
}

/// <summary>
/// Dónde se puede conseguir un objeto: tienda, biomas o ambos.
/// Es un enum de banderas (Flags), así que se pueden combinar con |.
/// </summary>
[Flags]
public enum OrigenObjeto
{
    Ninguno = 0,
    Tienda = 1,
    Biomas = 2,
    Ambos = Tienda | Biomas
}

/// <summary>Modos de juego disponibles.</summary>
public enum TipoModo
{
    Infinito,
    Niveles,
    Biomas
}
