using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance { get; private set; }

    public Tilemap walkable; // Tilemap que representa las áreas transitables
    public GameObject NodePrefab;
    public List<Node> nodes = new List<Node>(); // Lista de nodos en la cuadrícula

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateNodes();
        CreateConnetions();
    }

    void CreateNodes()
    {
        foreach (var pos in walkable.cellBounds.allPositionsWithin)
        {
            if (walkable.HasTile(pos))
            {
                Vector2 worldPos = walkable.CellToWorld(pos) + walkable.tileAnchor;
                Node node = Instantiate(NodePrefab, transform).GetComponent<Node>();
                node.transform.position = worldPos;
                nodes.Add(node);
            }
        }
    }

    void CreateConnetions()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                if (Vector2.Distance(nodes[i].transform.position, nodes[j].transform.position) < 1.5f)
                {
                    ConnectNodes(nodes[i], nodes[j]);
                    ConnectNodes(nodes[j], nodes[i]);
                }
            }
        }
    }

    void ConnectNodes(Node from, Node to)
    {
        if (from == to) return;
        from.neighbors.Add(to);
    }

    public List<Node> GeneratePath(Node start, Node goal)
    {
        List<Node> openSet = new List<Node> { start };

        foreach (Node node in nodes)
        {
            node.gScore = float.MaxValue;
        }

        // Inicializa los puntajes del nodo inicial
        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, goal.transform.position);
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            // Encuentra el nodo con el menor fScore en el openSet
            int lowestFIndex = default;
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fScore < openSet[lowestFIndex].fScore)
                {
                    lowestFIndex = i;
                }
            }

            // Obtiene el nodo actual y lo elimina del openSet
            Node current = openSet[lowestFIndex];
            openSet.RemoveAt(lowestFIndex);

            // Si llega al nodo objetivo, reconstruye el camino y termina temprano
            if (current == goal)
            {
                List<Node> path = new List<Node>();
                while (current != null)
                {
                    path.Add(current);
                    current = current.cameFrom;
                }
                path.Reverse();
                return path;
            }

            // Busca en los vecinos y actualiza el openSet
            foreach (Node neighbor in current.neighbors)
            {
                float heldGScore = current.gScore + Vector2.Distance(current.transform.position, neighbor.transform.position);

                if (heldGScore < neighbor.gScore)
                {
                    neighbor.cameFrom = current;
                    neighbor.gScore = heldGScore;
                    neighbor.hScore = Vector2.Distance(neighbor.transform.position, goal.transform.position);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }
}
