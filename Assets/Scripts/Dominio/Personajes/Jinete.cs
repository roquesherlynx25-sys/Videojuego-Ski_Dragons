/// <summary>
/// El jinete: personaje que lleva el nombre del usuario. No tiene
/// habilidad especial [SH], así que usa el comportamiento por defecto
/// heredado de Personaje.
/// </summary>
public class Jinete : Personaje
{
    /// <summary>Crea un jinete con el nombre del usuario y una vida.</summary>
    public Jinete(string nombreUsuario) : base(nombreUsuario)
    {
    }

    /// <summary>Crea un jinete con el nombre del usuario y vidas personalizadas.</summary>
    public Jinete(string nombreUsuario, int vidas) : base(nombreUsuario, vidas)
    {
    }

    /// <summary>Rol de esta clase.</summary>
    public override string Rol => "Jinete";
}
