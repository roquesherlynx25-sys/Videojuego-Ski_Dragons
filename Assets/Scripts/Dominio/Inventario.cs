using System;
using System.Collections.Generic;

/// <summary>
/// Todo lo que el jugador ha conseguido en la tienda y se guarda entre
/// partidas: personajes, biomas desbloqueados, niveles de habilidad,
/// Ángeles de reserva, runas y qué tiene equipado. Se puede convertir a
/// texto para guardarlo y volver a leerlo.
/// </summary>
public class Inventario
{
    public const string NombreJinete = "Jinete";
    public const string NombreAndary = "Andary";
    public const string NombreGorgon = "Gorgon";
    public const string ClaveTiempo = "Tiempo";
    public const string ClaveDureza = "Dureza";
    public const string NombreRunaEscudo = "RunaEscudo";
    public const string NombreRunaImanCodicia = "RunaImanCodicia";
    public const string NombreAngel = "Angel";

    /// <summary>Máximo de Ángeles de reserva que se pueden tener.</summary>
    public const int MaximoAngeles = 99;

    private readonly HashSet<string> personajes = new HashSet<string> { NombreJinete };
    private readonly HashSet<string> biomas = new HashSet<string> { "Sabana" };
    private readonly Dictionary<string, int> niveles = new Dictionary<string, int>();
    private readonly HashSet<string> runas = new HashSet<string>();
    private readonly HashSet<string> equipadas = new HashSet<string>();

    /// <summary>Ángeles de reserva (cada partida con el Ángel equipado gasta uno).</summary>
    public int AngelesConsumibles { get; private set; }

    // ---------- Personajes ----------
    public bool TienePersonaje(string nombre) { return personajes.Contains(nombre); }

    public void AgregarPersonaje(string nombre)
    {
        if (!string.IsNullOrWhiteSpace(nombre)) personajes.Add(nombre);
    }

    // ---------- Biomas ----------
    public bool TieneBioma(TipoBioma tipo) { return biomas.Contains(tipo.ToString()); }

    public void DesbloquearBioma(TipoBioma tipo) { biomas.Add(tipo.ToString()); }

    // ---------- Habilidades ----------
    /// <summary>Nivel guardado de una habilidad (1 si nunca se mejoró).</summary>
    public int NivelHabilidad(string habilidad)
    {
        int nivel;
        return niveles.TryGetValue(habilidad, out nivel) ? nivel : TablaHabilidades.NivelMinimo;
    }

    /// <summary>Sube un nivel la habilidad. Devuelve false si ya está en el máximo.</summary>
    public bool SubirNivelHabilidad(string habilidad)
    {
        int nivel = NivelHabilidad(habilidad);
        if (nivel >= TablaHabilidades.NivelMaximo) return false;

        niveles[habilidad] = nivel + 1;
        return true;
    }

    // ---------- Ángeles ----------
    public void AgregarAngel()
    {
        if (AngelesConsumibles < MaximoAngeles) AngelesConsumibles++;
    }

    /// <summary>Gasta un Ángel de reserva. Devuelve false si no hay.</summary>
    public bool UsarAngel()
    {
        if (AngelesConsumibles <= 0) return false;

        AngelesConsumibles--;
        return true;
    }

    // ---------- Runas ----------
    public bool TieneRuna(string runa) { return runas.Contains(runa); }

    public void AgregarRuna(string runa)
    {
        if (!string.IsNullOrWhiteSpace(runa)) runas.Add(runa);
    }

    // ---------- Equipado ----------
    public bool EstaEquipada(string nombre) { return equipadas.Contains(nombre); }

    public void CambiarEquipada(string nombre, bool equipada)
    {
        if (equipada) equipadas.Add(nombre);
        else equipadas.Remove(nombre);
    }

    // ---------- Guardado ----------
    /// <summary>Convierte el inventario a texto (para PlayerPrefs).</summary>
    public string Serializar()
    {
        List<string> niveleTexto = new List<string>();
        foreach (KeyValuePair<string, int> par in niveles)
        {
            niveleTexto.Add(par.Key + ":" + par.Value);
        }

        List<string> partes = new List<string>
        {
            "p=" + string.Join(",", personajes),
            "b=" + string.Join(",", biomas),
            "n=" + string.Join(",", niveleTexto),
            "a=" + AngelesConsumibles,
            "r=" + string.Join(",", runas),
            "e=" + string.Join(",", equipadas)
        };
        return string.Join(";", partes);
    }

    /// <summary>Lee un inventario guardado. Si el texto no sirve, devuelve uno nuevo.</summary>
    public static Inventario Deserializar(string texto)
    {
        Inventario inv = new Inventario();
        if (string.IsNullOrWhiteSpace(texto)) return inv;

        foreach (string parte in texto.Split(';'))
        {
            int igual = parte.IndexOf('=');
            if (igual <= 0) continue;

            string clave = parte.Substring(0, igual);
            string valor = parte.Substring(igual + 1);
            string[] items = valor.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            switch (clave)
            {
                case "p":
                    foreach (string item in items) inv.personajes.Add(item);
                    break;
                case "b":
                    foreach (string item in items) inv.biomas.Add(item);
                    break;
                case "n":
                    foreach (string item in items)
                    {
                        string[] par = item.Split(':');
                        int nivelLeido;
                        if (par.Length == 2 && int.TryParse(par[1], out nivelLeido))
                        {
                            inv.niveles[par[0]] = Math.Clamp(nivelLeido, TablaHabilidades.NivelMinimo, TablaHabilidades.NivelMaximo);
                        }
                    }
                    break;
                case "a":
                    int angeles;
                    if (int.TryParse(valor, out angeles))
                    {
                        inv.AngelesConsumibles = Math.Max(0, Math.Min(angeles, MaximoAngeles));
                    }
                    break;
                case "r":
                    foreach (string item in items) inv.runas.Add(item);
                    break;
                case "e":
                    foreach (string item in items) inv.equipadas.Add(item);
                    break;
            }
        }

        // Siempre se tienen el Jinete y la Sabana.
        inv.personajes.Add(NombreJinete);
        inv.biomas.Add("Sabana");
        return inv;
    }
}
