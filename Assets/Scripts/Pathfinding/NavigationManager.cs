using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance { get; private set; }

    public Tilemap walkable; // Tilemap que representa las áreas transitables
    public List<Node> nodes = new List<Node>(); // Lista de nodos en la cuadrícula
    public Dictionary<Vector3Int, Node> nodeMap = new Dictionary<Vector3Int, Node>(); // Diccionario para acceder a los nodos por su posición

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
                Node node = new(worldPos, pos);
                nodes.Add(node);
                nodeMap.Add(pos, node);
            }
        }
    }

    void CreateConnetions()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                if (Vector2.Distance(nodes[i].worldPosition, nodes[j].worldPosition) < 1.5f)
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
        from.neighbours.Add(to);
    }

    public List<Vector2> GeneratePath(Node start, Node goal)
    {
        List<Node> openSet = new List<Node> { start };

        foreach (Node node in nodes)
        {
            node.gScore = float.MaxValue;
            node.hScore = 0;
            node.cameFrom = null;
        }

        // Inicializa los puntajes del nodo inicial
        start.gScore = 0;
        start.hScore = Vector2.Distance(start.worldPosition, goal.worldPosition);

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
                List<Vector2> path = new List<Vector2>();
                while (current != null)
                {
                    path.Add(current.worldPosition);
                    current = current.cameFrom;
                }
                path.Reverse();
                return path;
            }

            // Busca en los vecinos y actualiza el openSet
            foreach (Node neighbor in current.neighbours)
            {
                float heldGScore = current.gScore + Vector2.Distance(current.worldPosition, neighbor.worldPosition);

                if (heldGScore < neighbor.gScore)
                {
                    neighbor.cameFrom = current;
                    neighbor.gScore = heldGScore;
                    neighbor.hScore = Vector2.Distance(neighbor.worldPosition, goal.worldPosition);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    public Node GetNodeAt(Vector3 worldPos)
    {
        Vector3Int cell = walkable.WorldToCell(worldPos);
        nodeMap.TryGetValue(cell, out Node node);
        if (node == null) Debug.LogError($"Failed to get node at: {worldPos.ToString()}");
        return node;
    }
}
