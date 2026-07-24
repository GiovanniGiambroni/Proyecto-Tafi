using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una celda en la cuadrícula para el algoritmo A*.
/// </summary>
public class Node
{
    public Vector2 worldPosition;
    public Vector3Int cellPosition;

    public Node cameFrom;
    public List<Node> neighbours = new List<Node>();

    public float gScore; // Costo desde el nodo inicial hasta este nodo
    public float hScore; // Costo estimado desde este nodo hasta el nodo objetivo
    public float fScore => gScore + hScore; // Costo total (gScore + hScore)

    public Node(Vector2 wPos, Vector3Int cPos)
    {
        worldPosition = wPos;
        cellPosition = cPos;
    }

    public override string ToString()
    {
        return worldPosition.ToString();
    }
}
