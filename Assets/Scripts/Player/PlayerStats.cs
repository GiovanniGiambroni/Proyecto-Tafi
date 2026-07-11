using System;

[Serializable]
public class PlayerStats
{
    public float MoveSpeed = 6f;
    public float Acceleration = 40f;
    public float Deceleration = 60f;

    public float DashDistance = 3;
    public float DashDuration = 1;
    public float DashCooldown = 1;
}
