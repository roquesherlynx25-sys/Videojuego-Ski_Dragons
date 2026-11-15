/// <summary>
/// Contrato que cumple toda habilidad del juego: se puede activar,
/// tiene nivel y un tiempo de recarga.
/// </summary>
public interface IHabilidad
{
    string Nombre { get; }
    int Nivel { get; }
    float Duracion { get; }
    float RecargaTotal { get; }
    float RecargaRestante { get; }
    bool EstaActiva { get; }
    bool EstaLista { get; }

    /// <summary>Intenta activar la habilidad. Devuelve false si no está lista.</summary>
    bool Activar();

    /// <summary>Hace avanzar el tiempo (duración y recarga).</summary>
    void Actualizar(float deltaTime);
}