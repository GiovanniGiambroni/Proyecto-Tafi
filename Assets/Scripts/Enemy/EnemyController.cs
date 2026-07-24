using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyState State { get; private set; }

    PlayerContext player;
    EnemyWave wave;

    float recalcTimer = 0.25f;
    Node playerNode, myNode;
    List<Vector2> pathToPlayer;

    NavigationManager nm;

    void Update()
    {
        if (!State.HasFlag(EnemyState.Alive)) return;

        if (recalcTimer > 0)
        {
            recalcTimer -= Time.deltaTime;
            return;
        }
        playerNode = nm.GetNodeAt(player.Body.position);
        myNode = nm.GetNodeAt(transform.position);
        pathToPlayer = nm.GeneratePath(myNode, playerNode);
        recalcTimer = 0.25f;
    }

    public void Spawn(PlayerContext player, EnemyWave wave)
    {
        this.player = player;
        this.wave = wave;
        nm = NavigationManager.Instance;

        State = EnemyState.Alive;
    }

    public void Die()
    {
        State = EnemyState.Dead;
        wave.CheckComplete();
    }

    public void ResetEnemy()
    {
        State = EnemyState.None;
        player = null;
        wave = null;
    }

    private void OnDrawGizmos()
    {
        if (pathToPlayer == null) return;
        Gizmos.color = Color.darkRed;
        for (int i = 1; i < pathToPlayer.Count; i++)
        {
            Gizmos.DrawLine(pathToPlayer[i-1], pathToPlayer[i]);
        }
    }
}
