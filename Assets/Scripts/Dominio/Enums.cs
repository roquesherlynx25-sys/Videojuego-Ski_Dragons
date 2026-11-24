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

/// <summary>Biomas del juego (sin acentos ni eñe para usarlos como clave).</summary>
public enum TipoBioma
{
    Sabana,
    Montanas,
    Grutas,
    Volcanes,
    Selva,
    Desierto,
    Tundra,
    Taiga,
    Marino
}

/// <summary>Qué le pasa al jugador cuando un enemigo lo toca.</summary>
public enum EfectoContacto
{
    Ninguno,
    RoboMonedas,
    MataDragon
}

/// <summary>Resultado de intentar comprar algo en la tienda.</summary>
public enum ResultadoCompra
{
    Exito,
    MonedasInsuficientes,
    YaAdquirido,
    NivelMaximo,
    NoDisponible
}
