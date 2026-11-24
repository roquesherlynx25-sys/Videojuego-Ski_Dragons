using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel para elegir el bioma del modo Biomas, armado por código. Muestra
/// los nueve biomas en una cuadrícula: los desbloqueados se eligen y los
/// bloqueados se compran con monedas.
/// </summary>
public class BiomasPanelUI
{
    private readonly RectTransform panel;
    private readonly TextMeshProUGUI textoMonedas;
    private readonly TextMeshProUGUI textoMensaje;
    private readonly Action<Bioma> alElegir;
    private GameObject[] celdas = new GameObject[0];

    public BiomasPanelUI(Transform padre, Action<Bioma> alElegir)
    {
        this.alElegir = alElegir;

        panel = UiRuntime.CrearPanel(padre, "PanelBiomas", new Vector2(1500f, 850f), new Color(0.04f, 0.08f, 0.16f, 0.98f));

        UiRuntime.CrearTexto(panel, "Titulo", "ELIGE UN BIOMA", 56f, new Vector2(0f, 360f),
            new Vector2(1000f, 80f), TextAlignmentOptions.Center, Color.white);
        textoMonedas = UiRuntime.CrearTexto(panel, "Monedas", "", 36f, new Vector2(0f, 300f),
            new Vector2(800f, 50f), TextAlignmentOptions.Center, new Color(1f, 0.85f, 0.2f));
        textoMensaje = UiRuntime.CrearTexto(panel, "Mensaje", "", 30f, new Vector2(-150f, -370f),
            new Vector2(1000f, 60f), TextAlignmentOptions.Center, new Color(1f, 0.92f, 0.4f));
        UiRuntime.CrearBoton(panel, "BtnCerrar", "Cerrar", new Vector2(600f, -370f), new Vector2(240f, 80f), Ocultar);

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
        foreach (GameObject celda in celdas)
        {
            if (celda != null) UnityEngine.Object.Destroy(celda);
        }

        EstadoJugador jugador = SesionJuego.Jugador;
        textoMonedas.text = "Monedas: " + jugador.Monedas;

        var biomas = CatalogoBiomas.Todos;
        celdas = new GameObject[biomas.Count];

        for (int i = 0; i < biomas.Count; i++)
        {
            Bioma bioma = biomas[i];
            float x = -480f + (i % 3) * 480f;
            float y = 190f - (i / 3) * 190f;

            string texto;
            if (!jugador.Inventario.TieneBioma(bioma.Tipo)) texto = bioma.Nombre + "\nDesbloquear (" + bioma.PrecioDesbloqueo + ")";
            else if (SesionJuego.BiomaSeleccionado != null && SesionJuego.BiomaSeleccionado.Tipo == bioma.Tipo) texto = bioma.Nombre + "\nElegido (sí)";
            else texto = bioma.Nombre + "\nElegir";

            Bioma actual = bioma;
            Button boton = UiRuntime.CrearBoton(panel, "BtnBioma" + i, texto, new Vector2(x, y),
                new Vector2(440f, 160f), () => Pulsar(actual));
            celdas[i] = boton.gameObject;
        }
    }

    private void Pulsar(Bioma bioma)
    {
        EstadoJugador jugador = SesionJuego.Jugador;

        if (!jugador.Inventario.TieneBioma(bioma.Tipo))
        {
            ResultadoCompra resultado = new ArticuloBioma(bioma).Comprar(jugador);

            if (resultado == ResultadoCompra.Exito)
            {
                Persistencia.Guardar(jugador);
                textoMensaje.text = "¡Desbloqueaste " + bioma.Nombre + "!";
            }
            else
            {
                textoMensaje.text = "No te alcanzan las monedas.";
            }

            Refrescar();
            return;
        }

        SesionJuego.BiomaSeleccionado = bioma;
        if (alElegir != null) alElegir(bioma);
        Ocultar();
    }
}
