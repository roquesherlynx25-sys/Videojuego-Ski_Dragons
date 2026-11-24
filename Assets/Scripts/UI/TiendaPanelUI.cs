using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel de la tienda, armado por código. Muestra el catálogo con su precio
/// y un botón para comprar cada artículo.
/// </summary>
public class TiendaPanelUI
{
    private readonly RectTransform panel;
    private readonly TextMeshProUGUI textoMonedas;
    private readonly TextMeshProUGUI textoMensaje;
    private readonly Action alCambiar;
    private GameObject[] filas = new GameObject[0];

    public TiendaPanelUI(Transform padre, Action alCambiar)
    {
        this.alCambiar = alCambiar;

        panel = UiRuntime.CrearPanel(padre, "PanelTienda", new Vector2(1500f, 950f), new Color(0.04f, 0.08f, 0.16f, 0.98f));

        UiRuntime.CrearTexto(panel, "Titulo", "TIENDA", 64f, new Vector2(0f, 410f),
            new Vector2(800f, 90f), TextAlignmentOptions.Center, Color.white);
        textoMonedas = UiRuntime.CrearTexto(panel, "Monedas", "", 40f, new Vector2(0f, 340f),
            new Vector2(800f, 60f), TextAlignmentOptions.Center, new Color(1f, 0.85f, 0.2f));
        textoMensaje = UiRuntime.CrearTexto(panel, "Mensaje", "", 32f, new Vector2(-150f, -410f),
            new Vector2(1000f, 70f), TextAlignmentOptions.Center, new Color(1f, 0.92f, 0.4f));
        UiRuntime.CrearBoton(panel, "BtnCerrar", "Cerrar", new Vector2(600f, -410f), new Vector2(240f, 80f), Ocultar);

        panel.gameObject.SetActive(false);
    }

    /// <summary>Abre la tienda.</summary>
    public void Mostrar()
    {
        textoMensaje.text = "";
        Refrescar();
        panel.gameObject.SetActive(true);
        panel.SetAsLastSibling();
    }

    /// <summary>Cierra la tienda.</summary>
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

        EstadoJugador jugador = SesionJuego.Jugador;
        textoMonedas.text = "Monedas: " + jugador.Monedas;

        var catalogo = Tienda.Instancia.Catalogo;
        filas = new GameObject[catalogo.Count * 2];

        for (int i = 0; i < catalogo.Count; i++)
        {
            ArticuloTienda articulo = catalogo[i];
            float y = 250f - i * 80f;

            TextMeshProUGUI descripcion = UiRuntime.CrearTexto(panel, "Fila" + i,
                articulo.Nombre + "  -  " + articulo.Detalle(jugador), 28f,
                new Vector2(-230f, y), new Vector2(1000f, 70f), TextAlignmentOptions.MidlineLeft, Color.white);
            filas[i * 2] = descripcion.gameObject;

            string etiqueta = TextoBoton(articulo, jugador);
            ArticuloTienda actual = articulo;
            Button boton = UiRuntime.CrearBoton(panel, "BtnFila" + i, etiqueta, new Vector2(560f, y),
                new Vector2(340f, 64f), () => Comprar(actual));
            filas[i * 2 + 1] = boton.gameObject;
        }
    }

    private static string TextoBoton(ArticuloTienda articulo, EstadoJugador jugador)
    {
        if (!articulo.Disponible(jugador)) return "Requiere otro objeto";
        if (articulo.YaAdquirido(jugador)) return articulo.ResultadoSiAdquirido == ResultadoCompra.NivelMaximo ? "Nivel máximo" : "Comprado";
        return "Comprar (" + articulo.Precio(jugador) + ")";
    }

    private void Comprar(ArticuloTienda articulo)
    {
        ResultadoCompra resultado = Tienda.Instancia.Comprar(articulo, SesionJuego.Jugador);

        switch (resultado)
        {
            case ResultadoCompra.Exito:
                textoMensaje.text = "¡Compraste: " + articulo.Nombre + "!";
                Persistencia.Guardar(SesionJuego.Jugador);
                break;
            case ResultadoCompra.MonedasInsuficientes:
                textoMensaje.text = "No te alcanzan las monedas.";
                break;
            case ResultadoCompra.NoDisponible:
                textoMensaje.text = "Primero necesitas el personaje o la runa.";
                break;
            default:
                textoMensaje.text = "Ya lo tienes.";
                break;
        }

        if (alCambiar != null) alCambiar();
        Refrescar();
    }
}
