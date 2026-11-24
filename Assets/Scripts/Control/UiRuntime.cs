using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Herramientas para crear paneles, textos y botones desde código. Se usan
/// para armar la tienda, los equipables y los biomas sin dibujarlos a mano
/// en el editor.
/// </summary>
public static class UiRuntime
{
    private static readonly Vector2 Centro = new Vector2(0.5f, 0.5f);

    /// <summary>Crea un panel de color en el centro de su padre.</summary>
    public static RectTransform CrearPanel(Transform padre, string nombre, Vector2 tamano, Color color)
    {
        GameObject objeto = new GameObject(nombre, typeof(RectTransform), typeof(Image));
        objeto.transform.SetParent(padre, false);

        RectTransform rect = objeto.GetComponent<RectTransform>();
        rect.anchorMin = Centro;
        rect.anchorMax = Centro;
        rect.pivot = Centro;
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = tamano;

        objeto.GetComponent<Image>().color = color;
        return rect;
    }

    /// <summary>Crea un texto de TextMeshPro.</summary>
    public static TextMeshProUGUI CrearTexto(Transform padre, string nombre, string texto, float tamanoFuente,
        Vector2 posicion, Vector2 tamano, TextAlignmentOptions alineacion, Color color)
    {
        GameObject objeto = new GameObject(nombre, typeof(RectTransform));
        objeto.transform.SetParent(padre, false);

        TextMeshProUGUI etiqueta = objeto.AddComponent<TextMeshProUGUI>();
        etiqueta.text = texto;
        etiqueta.fontSize = tamanoFuente;
        etiqueta.alignment = alineacion;
        etiqueta.color = color;
        etiqueta.raycastTarget = false;

        RectTransform rect = etiqueta.rectTransform;
        rect.anchorMin = Centro;
        rect.anchorMax = Centro;
        rect.pivot = Centro;
        rect.anchoredPosition = posicion;
        rect.sizeDelta = tamano;
        return etiqueta;
    }

    /// <summary>Crea un botón con texto que ajusta su tamaño de letra.</summary>
    public static Button CrearBoton(Transform padre, string nombre, string texto, Vector2 posicion, Vector2 tamano, UnityAction accion)
    {
        GameObject objeto = new GameObject(nombre, typeof(RectTransform), typeof(Image), typeof(Button));
        objeto.transform.SetParent(padre, false);

        RectTransform rect = objeto.GetComponent<RectTransform>();
        rect.anchorMin = Centro;
        rect.anchorMax = Centro;
        rect.pivot = Centro;
        rect.anchoredPosition = posicion;
        rect.sizeDelta = tamano;

        Image imagen = objeto.GetComponent<Image>();
        imagen.color = new Color(0.92f, 0.92f, 0.92f, 1f);

        Button boton = objeto.GetComponent<Button>();
        boton.targetGraphic = imagen;
        if (accion != null) boton.onClick.AddListener(accion);

        TextMeshProUGUI etiqueta = CrearTexto(objeto.transform, "Texto", texto, 30f, Vector2.zero,
            tamano - new Vector2(20f, 10f), TextAlignmentOptions.Center, new Color(0.1f, 0.1f, 0.1f, 1f));
        etiqueta.enableAutoSizing = true;
        etiqueta.fontSizeMin = 14f;
        etiqueta.fontSizeMax = 30f;
        return boton;
    }

    /// <summary>Cambia el texto de un botón creado con CrearBoton.</summary>
    public static void PonerTextoBoton(Button boton, string texto)
    {
        TextMeshProUGUI etiqueta = boton.GetComponentInChildren<TextMeshProUGUI>();
        if (etiqueta != null) etiqueta.text = texto;
    }
}
