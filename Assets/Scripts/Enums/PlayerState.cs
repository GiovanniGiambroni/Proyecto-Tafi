using System;

[Flags]
public enum PlayerState
{
    None = 0,
    Moving = 1 << 0,
    Dashing = 1 << 1,
    Casting = 1 << 2,
    Invulnerable = 1 << 3,
    Stunned = 1 << 4,
}
