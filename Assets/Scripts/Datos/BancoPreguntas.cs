using System;
using System.Collections.Generic;

/// <summary>
/// Banco de preguntas de POO (singleton). Entrega una pregunta aleatoria
/// cuando el jugador tiene 2 de los 3 fragmentos del Ángel.
/// </summary>
public class BancoPreguntas
{
    /// <summary>Única instancia del banco.</summary>
    public static BancoPreguntas Instancia { get; } = new BancoPreguntas();

    private readonly List<PreguntaPOO> preguntas = new List<PreguntaPOO>();
    private readonly Random azar = new Random();

    /// <summary>Cuántas preguntas hay cargadas.</summary>
    public int Cantidad => preguntas.Count;

    // Constructor privado: nadie más puede crear otro banco.
    private BancoPreguntas()
    {
        Agregar("¿Qué son las clases, desde el punto de vista de programación orientada a objetos?",
            0,
            "Son tipos de datos compuestos definidos por el usuario, que contiene datos y funciones miembros.",
            "Son datos definidos por el cliente, compuestos por bytes.",
            "Son datos simples definidos por el usuario, que contiene datos primitivos o normalizados.");

        Agregar("En sintaxis del lenguaje C++ una función cout es:",
            1,
            "Definir clase.",
            "Imprimir en pantalla.",
            "Definir un objeto.");

        Agregar("Agrupa lo esencial de los objetos e ignora lo irrelevante. A esto se le llama:",
            0,
            "Abstracción.",
            "Herencia.",
            "Polimorfismo.");

        Agregar("Desde el punto de vista POO un mensaje:",
            2,
            "No es una petición a un objeto para que haga algo.",
            "Se conforma de caracteres.",
            "Es una llamada a una función a través de un objeto.");

        Agregar("Entidad independiente que incorpora una estructura y un comportamiento establecido por la clase de la que proviene:",
            2,
            "Herencia.",
            "Sobrecarga de funciones.",
            "Instancia.");

        Agregar("Los clientes de una clase X son:",
            1,
            "Funciones que no tienen acceso a los elementos públicos de la clase X.",
            "Funciones que instancian objetos de la clase X.",
            "Funciones que tienen acceso a los elementos protegidos de la clase X.");

        Agregar("¿Qué es una clase en C++?",
            2,
            "Un tipo de dato primitivo que no guarda funciones.",
            "Un archivo que contiene un código fuente.",
            "Una plantilla o modelo para crear objetos.");

        Agregar("Desde el punto de vista de POO, los objetos son:",
            0,
            "Cosas o conceptos.",
            "Datos miembros.",
            "Un teclado, un mouse.");

        Agregar("Una función se hace amiga de una clase:",
            1,
            "Colocando la palabra reservada friend en el constructor de la clase.",
            "Anteponiendo la palabra reservada friend en el prototipo de la función colocada dentro de la clase.",
            "Colocando la palabra reservada friend en el encabezado de la clase.");

        Agregar("La sobrecarga de funciones:",
            0,
            "Se refiere a que existen varias funciones con el mismo nombre en donde la diferencia la marcan los parámetros en tipo, en número o en ambas.",
            "Se refiere a las funciones amigas.",
            "Se refiere a que existen varias funciones con el mismo nombre en donde la diferencia la establecen los objetos que las ejecutan.");

        Agregar("La redefinición de operadores se realiza a través de:",
            1,
            "Static.",
            "Operator.",
            "New.");

        Agregar("La diferencia entre la herencia simple y la herencia múltiple es:",
            0,
            "En la herencia simple se tiene a lo más una clase base o superclase y la herencia múltiple tiene a lo menos dos clases bases o superclases.",
            "En la herencia simple se tiene a lo menos una clase base o superclase y la herencia múltiple tiene a lo menos dos clases bases o superclases.",
            "En la herencia simple se tiene a lo más tres clases base o superclases y la herencia múltiple tiene a lo menos una clase base o superclase.");

        Agregar("Es correcto decir que:",
            1,
            "La herencia virtual hereda sus elementos al menos a dos clases.",
            "La herencia virtual trata de evitar la repetición de miembros que se heredan, de manera directa o indirecta, de dos o más clases.",
            "La herencia virtual y el polimorfismo son uno mismo.");

        Agregar("En herencia la etiqueta protected…",
            2,
            "Permite que los datos definidos bajo ella puedan ser modificados y consultados por los clientes de las clases derivadas.",
            "Permite que los datos definidos bajo ella sean de solo lectura para los miembros de las clases derivadas.",
            "Permite que los datos definidos bajo ella puedan ser modificados y consultados por los miembros de las clases derivadas.");

        Agregar("Una clase abstracta:",
            0,
            "Contiene al menos una función virtual pura.",
            "Se pueden crear objetos de ella a través del constructor.",
            "No debe contener una función virtual pura.");
    }

    /// <summary>Atajo interno para registrar una pregunta.</summary>
    private void Agregar(string texto, int indiceCorrecto, params string[] opciones)
    {
        preguntas.Add(new PreguntaPOO(texto, opciones, indiceCorrecto));
    }

    /// <summary>Devuelve una pregunta al azar.</summary>
    public PreguntaPOO ObtenerAleatoria()
    {
        return preguntas[azar.Next(preguntas.Count)];
    }

    /// <summary>Devuelve la pregunta de una posición (útil para pruebas).</summary>
    public PreguntaPOO Obtener(int indice)
    {
        if (indice < 0 || indice >= preguntas.Count)
            throw new ArgumentOutOfRangeException(nameof(indice));

        return preguntas[indice];
    }
}
