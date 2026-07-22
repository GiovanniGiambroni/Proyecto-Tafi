using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyState State { get; private set; }

    PlayerContext player;
    EnemyWave wave;

    public void Spawn(PlayerContext player, EnemyWave wave)
    {
        this.player = player;
        this.wave = wave;

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
}
