using System;
using UnityEngine;

/// <summary>
/// Representa las estadísticas del jugador. Únicamente expone valores.
/// </summary>
[Serializable]
public class PlayerStats
{
    [Header("Movement")]
    public float MoveSpeed = 10;
    public float Acceleration = 18;
    public float Deceleration = 30;
    public float BumpThreshold = 0.25f;
    public float BumpDecay = 0.2f;

    [Header("Dash")]
    public float DashDistance = 3;
    public float DashDuration = 0.2f;
    public float DashStaminaCost = 10;

    [Header("Resources")]
    public int MaxHealth = 3;
    public float MaxEnergy = 60;
    public float EnergyRegenRate = 2.5f;
    public float MaxStamina = 30;
    public float StaminaRegenRate = 5;
}
