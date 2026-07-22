using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una zona de spawn para el jugador en el juego. Contiene un punto de spawn inicial y una lista de checkpoints donde el jugador puede reaparecer.
/// </summary>
public class PlayerSpawnZone : MonoBehaviour
{
    public GameObject player;
    public List<Transform> checkPoints = new List<Transform>();

    PlayerController playerController;
    Transform currentCheckPoint;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        checkPoints.Add(transform);
        SpawnResetPlayer();
    }

    public void SpawnResetPlayer()
    {
        player.transform.position = transform.position;
        playerController.StartResetPlayer();
    }

    public void SpawnPlayerAtCheckPoint()
    {
        if (currentCheckPoint != null)
        {
            player.transform.position = currentCheckPoint.position;
            playerController.StartResetPlayer();
        }
        else
        {
            Debug.LogWarning("No checkpoint set for player spawn.");
        }
    }
}
