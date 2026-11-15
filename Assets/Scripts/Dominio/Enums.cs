using System;

/// <summary>Estados posibles de una partida.</summary>
public enum EstadoPartida
{
    Jugando,
    GameOver
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