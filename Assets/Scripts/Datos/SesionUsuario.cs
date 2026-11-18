using System;

/// <summary>
/// Datos de una sesión de uso: quién entró, a qué hora, a qué hora salió
/// y cuánto tiempo permaneció en la aplicación.
/// </summary>
public class SesionUsuario
{
    /// <summary>Usuario que inició sesión.</summary>
    public string Usuario { get; }

    /// <summary>Momento en que entró a la aplicación.</summary>
    public DateTime HoraEntrada { get; }

    /// <summary>Momento en que salió (null mientras la sesión sigue abierta).</summary>
    public DateTime? HoraSalida { get; private set; }

    /// <summary>True si ya se registró la salida.</summary>
    public bool Cerrada => HoraSalida.HasValue;

    /// <summary>Tiempo de permanencia (hasta ahora, si la sesión sigue abierta).</summary>
    public TimeSpan Permanencia
    {
        get
        {
            DateTime fin = HoraSalida ?? DateTime.Now;
            return fin - HoraEntrada;
        }
    }

    /// <summary>Permanencia como texto HH:MM:SS.</summary>
    public string PermanenciaTexto =>
        $"{(int)Permanencia.TotalHours:00}:{Permanencia.Minutes:00}:{Permanencia.Seconds:00}";

    /// <summary>Abre una sesión con la hora actual como entrada.</summary>
    public SesionUsuario(string usuario) : this(usuario, DateTime.Now)
    {
    }

    /// <summary>Abre una sesión con una hora de entrada dada (sobrecarga).</summary>
    public SesionUsuario(string usuario, DateTime horaEntrada)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            throw new ArgumentException("El usuario no puede estar vacío.");

        Usuario = usuario.Trim();
        HoraEntrada = horaEntrada;
    }

    /// <summary>Registra la salida con la hora actual.</summary>
    public void RegistrarSalida()
    {
        RegistrarSalida(DateTime.Now);
    }

    /// <summary>Registra la salida con una hora dada (sobrecarga).</summary>
    public void RegistrarSalida(DateTime horaSalida)
    {
        if (Cerrada) return;

        if (horaSalida < HoraEntrada)
            throw new ArgumentException("La hora de salida no puede ser anterior a la de entrada.");

        HoraSalida = horaSalida;
    }

    /// <summary>Línea que se escribe en la bitácora.</summary>
    public string ResumenBitacora()
    {
        string salida = HoraSalida.HasValue
            ? HoraSalida.Value.ToString("dd/MM/yyyy HH:mm:ss")
            : "(sesión abierta)";

        return "Usuario: " + Usuario +
               " | Entrada: " + HoraEntrada.ToString("dd/MM/yyyy HH:mm:ss") +
               " | Salida: " + salida +
               " | Permanencia: " + PermanenciaTexto;
    }

    public override string ToString()
    {
        return ResumenBitacora();
    }
}
