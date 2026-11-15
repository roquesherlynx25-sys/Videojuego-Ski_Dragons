using System;
using UnityEngine;
using UnityEngine.SceneManagement;

///Controla el estado de la partida: puntos, fin del juego y reinicio.
///Es un singleton: solo existe uno y se accede con GameManager.Instancia.

public class GameManager : MonoBehaviour
{
    ///Única instancia del GameManager en la escena.
    public static GameManager Instancia { get; private set; }

    ///Estado actual de la partida.
    public EstadoPartida Estado { get; private set; } = EstadoPartida.Jugando;

    ///Puntos acumulados (una torre superada = 1 punto).
    public int Puntos { get; private set; }

    ///Evento: avisa a quien escuche que cambiaron los puntos.
    public event Action<int> OnPuntosCambiados;

    ///Evento: avisa que la partida terminó.
    public event Action OnGameOver;

    private void Awake()
    {
        // Si ya existe otra instancia, esta sobra y se elimina.
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        Time.timeScale = 1f; // Por si venimos de un Game Over (tiempo congelado)
    }

    private void Update()
    {
        // En Game Over, la tecla R reinicia la partida.
        if (Estado == EstadoPartida.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            Reiniciar();
        }
    }

    ///Suma un punto, solo si la partida sigue en curso.
    public void SumarPunto()
    {
        if (Estado != EstadoPartida.Jugando) return;

        Puntos++;
        OnPuntosCambiados?.Invoke(Puntos);
    }

    ///Termina la partida y congela el juego.
    public void TerminarPartida()
    {
        if (Estado == EstadoPartida.GameOver) return;

        Estado = EstadoPartida.GameOver;
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    ///Recarga la escena actual para empezar de cero.
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}