/// <summary>
/// Modo Infinito: no tiene final. El progreso se muestra solo con los
/// puntos (obstáculos superados) y las monedas recogidas.
/// </summary>
public class ModoInfinito : ModoJuego
{
    public override string Nombre => "Infinito";

    public override string TextoProgreso()
    {
        return $"Puntos: {Puntos}   Monedas: {MonedasRecogidas}";
    }
}
