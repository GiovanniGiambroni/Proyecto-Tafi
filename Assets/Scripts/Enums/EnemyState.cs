using System;

[Flags]
public enum EnemyState
{
    None = 0,
    Alive = 1 << 0,
    Chasing = 1 << 1,
    Attacking = 1 << 2,
    Stunned = 1 << 3,
    Dead = 1 << 4
}
