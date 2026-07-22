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
    }

    void CreateNodes()
    {
        foreach (var pos in walkable.cellBounds.allPositionsWithin)
        {
            if (walkable.HasTile(pos))
            {
                Vector3 worldPos = walkable.CellToWorld(pos) + walkable.tileAnchor;
                Node node = Instantiate(NodePrefab).AddComponent<Node>();
                node.transform.position = worldPos;
                nodes.Add(node);
            }
        }
    }

    void CreateConnetions()
    {

    }

    void ConnectNodes(Node from, Node to)
    {
        //if ()
    }
}
