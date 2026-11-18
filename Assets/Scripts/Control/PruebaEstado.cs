using UnityEngine;

/// <summary>Script temporal para comprobar EstadoJugador y los objetos.</summary>
public class PruebaEstado : MonoBehaviour
{
    private void Start()
    {
        EstadoJugador jugador = new EstadoJugador();
        jugador.PersonajeSeleccionado = new DragonGorgon();
        Debug.Log(jugador);

        ObjetoJuego moneda = new Moneda(5);
        Debug.Log(moneda);
        moneda.AlRecoger(jugador); // +5
        Debug.Log("Tras recoger: " + jugador.Monedas);

        jugador.MultiplicadorMonedas = 2; // como una runa de duplicar
        moneda.AlRecoger(jugador); // +10
        Debug.Log("Con multiplicador x2: " + jugador.Monedas);

        Debug.Log("Gastar 100: " + jugador.GastarMonedas(100));
        Debug.Log("Gastar 15: " + jugador.GastarMonedas(15));
        Debug.Log("Monedas restantes: " + jugador.Monedas);

        jugador.AgregarFragmentoAngel();
        jugador.AgregarFragmentoAngel();
        Debug.Log(jugador);
    }
}