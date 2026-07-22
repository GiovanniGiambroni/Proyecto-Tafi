using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Representa una oleada de enemigos dentro de una zona de enemigos.
/// </summary>
public class EnemyWave : MonoBehaviour
{
    List<EnemyController> enemies = new List<EnemyController>();

    EnemyZone parentZone;

    void Awake()
    {
        parentZone = GetComponentInParent<EnemyZone>();
        enemies = GetComponentsInChildren<EnemyController>().ToList();
    }

    public void Activate(PlayerContext player)
    {
        foreach (var enemy in enemies)
        {
            enemy.Spawn(player, this);
        }
    }

    public void CheckComplete()
    {
        if (!enemies.Any(e => e.State.HasFlag(EnemyState.Alive))) parentZone.OnWaveCompleted();
    }

    public void ResetWave()
    {
        foreach (var enemy in enemies)
        {
            enemy.ResetEnemy();
        }
    }
}
