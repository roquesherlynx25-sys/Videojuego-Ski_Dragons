using System;

/// <summary>Resultado de validar el acceso.</summary>
public enum ResultadoLogin
{
    Correcto,
    CamposVacios,
    Incorrecto
}

/// <summary>
/// Valida el acceso a la aplicación con un usuario y una clave, los
/// mismos para todos los jugadores. Por defecto: jinete / dragon2026.
/// </summary>
public class Autenticador
{
    /// <summary>Usuario válido por defecto.</summary>
    public const string UsuarioPorDefecto = "jinete";

    /// <summary>Clave válida por defecto.</summary>
    public const string ClavePorDefecto = "dragon2026";

    private readonly string usuarioValido;
    private readonly string claveValida;

    /// <summary>Crea el autenticador con las credenciales por defecto.</summary>
    public Autenticador() : this(UsuarioPorDefecto, ClavePorDefecto)
    {
    }

    /// <summary>Crea el autenticador con credenciales propias (sobrecarga).</summary>
    public Autenticador(string usuario, string clave)
    {
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrEmpty(clave))
            throw new ArgumentException("El usuario y la clave válidos no pueden estar vacíos.");

        usuarioValido = usuario.Trim();
        claveValida = clave;
    }

    /// <summary>
    /// Compara lo escrito con las credenciales válidas. El usuario no
    /// distingue mayúsculas; la clave sí.
    /// </summary>
    public ResultadoLogin Validar(string usuario, string clave)
    {
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrEmpty(clave))
            return ResultadoLogin.CamposVacios;

        bool usuarioOk = string.Equals(usuario.Trim(), usuarioValido, StringComparison.OrdinalIgnoreCase);
        bool claveOk = clave == claveValida;

        return (usuarioOk && claveOk) ? ResultadoLogin.Correcto : ResultadoLogin.Incorrecto;
    }
}
