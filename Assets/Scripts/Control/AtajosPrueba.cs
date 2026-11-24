using UnityEngine;

/// <summary>
/// TEMPORAL: atajos de teclado para probar sin esperar. Solo funcionan
/// dentro del editor de Unity (no en el ejecutable).
/// 1 = +1 fragmento de Ángel · 3 = +500 monedas · 4 = reiniciar progreso
/// 5 = completar el nivel actual (modo Niveles)
/// 6 = oveja · 7 = Grifo · 8 = Goblin
/// Borrar este script antes de entregar.
/// </summary>
public class AtajosPrueba : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        EstadoJugador jugador = SesionJuego.Jugador;
        GameManager gm = GameManager.Instancia;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jugador.AgregarFragmentoAngel();
            Debug.Log("[Atajo] " + jugador);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            jugador.AgregarMonedas(500);
            Persistencia.Guardar(jugador);
            Debug.Log("[Atajo] " + jugador);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            jugador.Cargar(0, 0);
            jugador.CargarInventario(new Inventario());
            Persistencia.BorrarTodo();
            Debug.Log("[Atajo] Progreso reiniciado. " + jugador);
        }

        if (gm == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha5) && gm.Modo is ModoNiveles niveles)
        {
            niveles.SaltarNivel();
            gm.ComprobarVictoria();
            Debug.Log("[Atajo] " + niveles);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ElementosJuego.CrearOveja(new Vector3(9f, -3f, 0f), gm.Modo.RecompensaOveja);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ElementosJuego.CrearEnemigo(new Grifo(), new Vector3(9f, 1f, 0f), gm.Modo.VelocidadEnemigo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ElementosJuego.CrearEnemigo(new Goblin(), new Vector3(9f, -4.2f, 0f), gm.Modo.VelocidadEnemigo);
        }
#endif
    }
}
