using System.IO;
using UnityEngine;

/// <summary>
/// Lleva la sesión del usuario durante toda la aplicación (singleton que
/// sobrevive entre escenas). Registra la hora de entrada al iniciar
/// sesión y, al cerrar la aplicación, la hora de salida y la permanencia.
/// </summary>
public class SessionManager : MonoBehaviour
{
    /// <summary>Única instancia del SessionManager.</summary>
    public static SessionManager Instancia { get; private set; }

    /// <summary>Sesión actual (null hasta que el usuario inicia sesión).</summary>
    public SesionUsuario Sesion { get; private set; }

    private Bitacora bitacora;
    private bool bitacoraEscrita;

    private void Awake()
    {
        // Si ya existe otro SessionManager, este sobra.
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);

        string ruta = Path.Combine(Application.persistentDataPath, "bitacora.txt");
        bitacora = new Bitacora(ruta);
    }

    /// <summary>Abre una sesión nueva con la hora actual como entrada.</summary>
    public void IniciarSesion(string usuario)
    {
        Sesion = new SesionUsuario(usuario);
        bitacoraEscrita = false;
        SesionJuego.NombreUsuario = Sesion.Usuario;

        Debug.Log("Sesión iniciada: " + Sesion.Usuario + " a las " + Sesion.HoraEntrada.ToString("HH:mm:ss"));
    }

    /// <summary>
    /// Registra la salida y escribe la bitácora. Solo lo hace una vez,
    /// aunque se llame desde el botón Salir y al cerrar la aplicación.
    /// </summary>
    public void CerrarSesion()
    {
        if (Sesion == null || bitacoraEscrita) return;

        Sesion.RegistrarSalida();
        bool escrita = bitacora.Registrar(Sesion);
        bitacoraEscrita = true;

        if (escrita)
        {
            Debug.Log("Bitácora guardada en: " + bitacora.Ruta + "\n" + Sesion.ResumenBitacora());
        }
        else
        {
            Debug.LogWarning("No se pudo escribir la bitácora: " + bitacora.UltimoError);
        }
    }

    /// <summary>Unity lo llama al cerrar la aplicación (o al detener Play en el editor).</summary>
    private void OnApplicationQuit()
    {
        CerrarSesion();
    }
}
