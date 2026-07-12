using System;

/// <summary>
/// Representa las estadísticas del jugador. Únicamente expone valores.
/// </summary>
[Serializable]
public class PlayerStats
{
    public float MoveSpeed = 10;
    public float Acceleration = 18;
    public float Deceleration = 30;

    public float DashDistance = 3;
    public float DashDuration = 1;
    public float DashCooldown = 1;
}
