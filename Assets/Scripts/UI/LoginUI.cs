using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pantalla de inicio de sesión: pide usuario y clave, los valida con el
/// Autenticador y, si son correctos, abre el menú principal.
/// </summary>
public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField campoUsuario;
    [SerializeField] private TMP_InputField campoClave;
    [SerializeField] private TextMeshProUGUI textoMensaje;
    [SerializeField] private string nombreEscenaMenu = "MenuPrincipal";

    private readonly Autenticador autenticador = new Autenticador();

    private void Start()
    {
        textoMensaje.text = "";

        // Si todavía no existe el SessionManager, se crea aquí.
        if (SessionManager.Instancia == null)
        {
            new GameObject("SessionManager").AddComponent<SessionManager>();
        }

        campoUsuario.Select();
    }

    private void Update()
    {
        // La tecla Enter equivale a pulsar el botón Entrar.
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Entrar();
        }
    }

    /// <summary>Lo llama el botón Entrar.</summary>
    public void Entrar()
    {
        ResultadoLogin resultado = autenticador.Validar(campoUsuario.text, campoClave.text);

        switch (resultado)
        {
            case ResultadoLogin.Correcto:
                SessionManager.Instancia.IniciarSesion(campoUsuario.text);
                SceneManager.LoadScene(nombreEscenaMenu);
                break;

            case ResultadoLogin.CamposVacios:
                textoMensaje.text = "Escribe tu usuario y tu clave.";
                break;

            default:
                textoMensaje.text = "Usuario o clave incorrectos.";
                campoClave.text = "";
                break;
        }
    }
}
