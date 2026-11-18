using UnityEngine;

/// <summary>
/// TEMPORAL: atajos de teclado para probar el Ángel sin la tienda.
/// Solo funcionan dentro del editor de Unity (no en el ejecutable).
/// 1 = +1 fragmento · 2 = equipar Ángel · 3 = +10 monedas · 4 = reiniciar progreso
/// Borrar este script cuando exista la tienda.
/// </summary>
public class AtajosPrueba : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        EstadoJugador jugador = SesionJuego.Jugador;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jugador.AgregarFragmentoAngel();
            Debug.Log("[Atajo] " + jugador);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (jugador.ObtenerEquipado<Angel>() == null)
            {
                jugador.Equipar(new Angel());
            }
            Debug.Log("[Atajo] Ángel equipado. " + jugador);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            jugador.AgregarMonedas(10);
            Debug.Log("[Atajo] " + jugador);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            jugador.Cargar(0, 0);
            Persistencia.BorrarTodo();
            Debug.Log("[Atajo] Progreso reiniciado. " + jugador);
        }
#endif
    }
}
