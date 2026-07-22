using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del nivel, contiene las zonas y les da ordenes de alto nivel.
/// </summary>
public class LevelController : MonoBehaviour
{
    public PlayerSpawnZone PlayerSpawnZone;
    public List<EnemyZone> EnemyZones = new List<EnemyZone>();
    public List<BossZone> BossZones = new List<BossZone>();
    public WinZone WinZone;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        PlayerSpawnZone.SpawnResetPlayer();
        foreach (var zone in EnemyZones)
        {
            zone.ResetZone();
        }
    }
}
