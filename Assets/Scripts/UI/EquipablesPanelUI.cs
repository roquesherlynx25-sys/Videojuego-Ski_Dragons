using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel de objetos equipables, armado por código. Permite equipar o
/// quitar el Ángel de reserva y las dos runas que se compraron.
/// </summary>
public class EquipablesPanelUI
{
    private readonly RectTransform panel;
    private readonly TextMeshProUGUI textoMensaje;
    private readonly Action alCambiar;
    private GameObject[] filas = new GameObject[0];

    public EquipablesPanelUI(Transform padre, Action alCambiar)
    {
        this.alCambiar = alCambiar;

        panel = UiRuntime.CrearPanel(padre, "PanelEquipablesRuntime", new Vector2(1400f, 700f), new Color(0.04f, 0.08f, 0.16f, 0.98f));

        UiRuntime.CrearTexto(panel, "Titulo", "OBJETOS EQUIPABLES", 56f, new Vector2(0f, 290f),
            new Vector2(1000f, 80f), TextAlignmentOptions.Center, Color.white);
        UiRuntime.CrearTexto(panel, "Nota", "Los dragones queman ovejas; el Jinete no. Compra más objetos en la tienda.", 26f,
            new Vector2(0f, 220f), new Vector2(1200f, 50f), TextAlignmentOptions.Center, new Color(0.8f, 0.85f, 1f));
        textoMensaje = UiRuntime.CrearTexto(panel, "Mensaje", "", 30f, new Vector2(-150f, -300f),
            new Vector2(900f, 60f), TextAlignmentOptions.Center, new Color(1f, 0.92f, 0.4f));
        UiRuntime.CrearBoton(panel, "BtnCerrar", "Cerrar", new Vector2(520f, -300f), new Vector2(240f, 80f), Ocultar);

        panel.gameObject.SetActive(false);
    }

    /// <summary>Abre el panel.</summary>
    public void Mostrar()
    {
        textoMensaje.text = "";
        Refrescar();
        panel.gameObject.SetActive(true);
        panel.SetAsLastSibling();
    }

    /// <summary>Cierra el panel.</summary>
    public void Ocultar()
    {
        panel.gameObject.SetActive(false);
    }

    private void Refrescar()
    {
        foreach (GameObject fila in filas)
        {
            if (fila != null) UnityEngine.Object.Destroy(fila);
        }

        Inventario inv = SesionJuego.Jugador.Inventario;
        filas = new GameObject[6];

        CrearFila(0, 130f, "Ángel de reserva (tienes " + inv.AngelesConsumibles + "): empiezas con 1/3 fragmentos",
            Inventario.NombreAngel, inv.AngelesConsumibles > 0 || inv.EstaEquipada(Inventario.NombreAngel));
        CrearFila(1, 20f, "Runa de Escudo (nivel " + inv.NivelHabilidad(Inventario.NombreRunaEscudo) + "): escudo con la tecla Q",
            Inventario.NombreRunaEscudo, inv.TieneRuna(Inventario.NombreRunaEscudo));
        CrearFila(2, -90f, "Runa de Imán / Codicia: atrae monedas y reduce el robo del Grifo",
            Inventario.NombreRunaImanCodicia, inv.TieneRuna(Inventario.NombreRunaImanCodicia));
    }

    private void CrearFila(int indice, float y, string texto, string clave, bool disponible)
    {
        Inventario inv = SesionJuego.Jugador.Inventario;

        TextMeshProUGUI descripcion = UiRuntime.CrearTexto(panel, "FilaEq" + indice, texto, 28f,
            new Vector2(-230f, y), new Vector2(900f, 90f), TextAlignmentOptions.MidlineLeft, Color.white);
        filas[indice * 2] = descripcion.gameObject;

        string etiqueta = !disponible ? "No lo tienes" : inv.EstaEquipada(clave) ? "Equipado (sí)" : "Equipar";
        Button boton = UiRuntime.CrearBoton(panel, "BtnEq" + indice, etiqueta, new Vector2(500f, y),
            new Vector2(300f, 70f), () => Alternar(clave, disponible));
        filas[indice * 2 + 1] = boton.gameObject;
    }

    private void Alternar(string clave, bool disponible)
    {
        if (!disponible)
        {
            textoMensaje.text = "Cómpralo primero en la tienda.";
            return;
        }

        Inventario inv = SesionJuego.Jugador.Inventario;
        bool nuevoEstado = !inv.EstaEquipada(clave);
        inv.CambiarEquipada(clave, nuevoEstado);
        Persistencia.Guardar(SesionJuego.Jugador);

        textoMensaje.text = nuevoEstado ? "Equipado." : "Quitado.";
        if (alCambiar != null) alCambiar();
        Refrescar();
    }
}
