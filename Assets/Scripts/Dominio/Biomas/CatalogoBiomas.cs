using System.Collections.Generic;

/// <summary>Lista de todos los biomas del juego, en orden de progresión.</summary>
public static class CatalogoBiomas
{
    /// <summary>Todos los biomas, del más fácil al más difícil.</summary>
    public static IReadOnlyList<Bioma> Todos { get; } = new List<Bioma>
    {
        new Sabana(),
        new Montanas(),
        new Grutas(),
        new Volcanes(),
        new Selva(),
        new Desierto(),
        new Tundra(),
        new Taiga(),
        new Marino()
    };

    /// <summary>Busca un bioma por su tipo (la Sabana si no lo encuentra).</summary>
    public static Bioma Obtener(TipoBioma tipo)
    {
        foreach (Bioma bioma in Todos)
        {
            if (bioma.Tipo == tipo) return bioma;
        }
        return Todos[0];
    }
}
