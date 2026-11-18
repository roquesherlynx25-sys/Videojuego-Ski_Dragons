using UnityEngine;

/// <summary>
/// Guarda y carga el progreso del jugador con PlayerPrefs:
/// monedas, fragmentos de Ángel y récord del modo Infinito.
/// </summary>
public static class Persistencia
{
    private const string ClaveMonedas = "sd_monedas";
    private const string ClaveFragmentos = "sd_fragmentos";
    private const string ClaveRecordInfinito = "sd_record_infinito";

    /// <summary>Carga monedas y fragmentos guardados en el jugador.</summary>
    public static void Cargar(EstadoJugador jugador)
    {
        jugador.Cargar(PlayerPrefs.GetInt(ClaveMonedas, 0),
                       PlayerPrefs.GetInt(ClaveFragmentos, 0));
    }

    /// <summary>Guarda monedas y fragmentos del jugador.</summary>
    public static void Guardar(EstadoJugador jugador)
    {
        PlayerPrefs.SetInt(ClaveMonedas, jugador.Monedas);
        PlayerPrefs.SetInt(ClaveFragmentos, jugador.FragmentosAngel);
        PlayerPrefs.Save();
    }

    /// <summary>Récord guardado del modo Infinito.</summary>
    public static int CargarRecordInfinito()
    {
        return PlayerPrefs.GetInt(ClaveRecordInfinito, 0);
    }

    /// <summary>Guarda el récord solo si supera al anterior. Devuelve si es nuevo récord.</summary>
    public static bool GuardarRecordInfinito(int puntos)
    {
        if (puntos <= CargarRecordInfinito()) return false;

        PlayerPrefs.SetInt(ClaveRecordInfinito, puntos);
        PlayerPrefs.Save();
        return true;
    }

    /// <summary>Borra todo el progreso guardado (útil para pruebas).</summary>
    public static void BorrarTodo()
    {
        PlayerPrefs.DeleteKey(ClaveMonedas);
        PlayerPrefs.DeleteKey(ClaveFragmentos);
        PlayerPrefs.DeleteKey(ClaveRecordInfinito);
        PlayerPrefs.Save();
    }
}
