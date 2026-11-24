using System;

/// <summary>
/// Modo Infinito: no tiene final. El progreso se muestra con los puntos
/// (obstáculos superados) y las monedas. Cada cierto número de puntos
/// sube la dificultad: más velocidad, columnas más juntas y enemigos más
/// frecuentes y más rápidos (con topes para que no sea imposible).
/// </summary>
public class ModoInfinito : ModoJuego
{
    /// <summary>Puntos que hay que sumar para subir un escalón de dificultad.</summary>
    public const int PuntosPorEscalon = 50;

    private int Escalon => Puntos / PuntosPorEscalon;

    public override string Nombre => "Infinito";

    public override float MultiplicadorVelocidad => Math.Min(1.9f, 1f + 0.05f * Escalon);

    public override float SeparacionColumnas => Math.Max(5.2f, 6.5f - 0.1f * Escalon);

    public override bool HayEnemigos => true;

    public override int MinColumnasEnemigo => Math.Max(2, (int)Math.Round(6f - 0.5f * Escalon));

    public override int MaxColumnasEnemigo => Math.Max(MinColumnasEnemigo, (int)Math.Round(8f - 0.5f * Escalon));

    public override float ProbabilidadGrifo => 0.5f;

    public override float FactorVelocidadEnemigo => Math.Min(2.4f, 1.1f + 0.07f * Escalon);

    public override bool HayOvejas => true;

    public override string TextoProgreso()
    {
        return $"Puntos: {Puntos}   Monedas: {MonedasRecogidas}";
    }
}
