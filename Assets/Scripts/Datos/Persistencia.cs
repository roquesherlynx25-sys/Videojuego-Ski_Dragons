using UnityEngine;

/// <summary>
/// Guarda y carga el progreso del jugador con PlayerPrefs: monedas,
/// inventario (lo comprado en la tienda) y récord del modo Infinito.
/// </summary>
public static class Persistencia
{
    private const string ClaveMonedas = "sd_monedas";
    private const string ClaveInventario = "sd_inventario";
    private const string ClaveRecordInfinito = "sd_record_infinito";

    /// <summary>Carga monedas e inventario guardados en el jugador.</summary>
    public static void Cargar(EstadoJugador jugador)
    {
        jugador.Cargar(PlayerPrefs.GetInt(ClaveMonedas, 0), 0);
        jugador.CargarInventario(Inventario.Deserializar(PlayerPrefs.GetString(ClaveInventario, "")));
    }

    /// <summary>Guarda monedas e inventario del jugador.</summary>
    public static void Guardar(EstadoJugador jugador)
    {
        PlayerPrefs.SetInt(ClaveMonedas, jugador.Monedas);
        PlayerPrefs.SetString(ClaveInventario, jugador.Inventario.Serializar());
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
        PlayerPrefs.DeleteKey(ClaveInventario);
        PlayerPrefs.DeleteKey(ClaveRecordInfinito);
        PlayerPrefs.Save();
    }
}
