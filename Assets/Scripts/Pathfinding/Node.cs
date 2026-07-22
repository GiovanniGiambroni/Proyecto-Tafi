using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una celda en la cuadrícula para el algoritmo A*.
/// </summary>
public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> neighbors = new List<Node>();

    public float gScore; // Costo desde el nodo inicial hasta este nodo
    public float hScore; // Costo estimado desde este nodo hasta el nodo objetivo
    public float fScore => gScore + hScore; // Costo total (gScore + hScore)
}
