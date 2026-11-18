using System;
using System.IO;

/// <summary>
/// Escribe en un archivo de texto la bitácora de sesiones
/// (usuario, hora de entrada, hora de salida y permanencia).
/// </summary>
public class Bitacora
{
    /// <summary>Ruta del archivo de bitácora.</summary>
    public string Ruta { get; }

    /// <summary>Mensaje del último error de escritura (vacío si no hubo).</summary>
    public string UltimoError { get; private set; } = "";

    public Bitacora(string ruta)
    {
        if (string.IsNullOrWhiteSpace(ruta))
            throw new ArgumentException("La bitácora necesita una ruta.");

        Ruta = ruta;
    }

    /// <summary>
    /// Agrega una línea con los datos de la sesión.
    /// Devuelve true si se pudo escribir, false si hubo un error de archivo.
    /// </summary>
    public bool Registrar(SesionUsuario sesion)
    {
        if (sesion == null) throw new ArgumentNullException(nameof(sesion));

        try
        {
            string carpeta = Path.GetDirectoryName(Ruta);
            if (!string.IsNullOrEmpty(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            File.AppendAllText(Ruta, sesion.ResumenBitacora() + Environment.NewLine);
            UltimoError = "";
            return true;
        }
        catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
        {
            UltimoError = e.Message;
            return false;
        }
    }
}
