using UnityEngine;

/// <summary>
/// Centraliza el acceso a los componentes y datos del jugador, proporcionando una interfaz unificada para interactuar con ellos.
/// </summary>
public class PlayerContext
{
    public Rigidbody2D Body {  get; private set; }
    public PlayerStats Stats { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerResources Resources { get; private set; }
    public PlayerUI UI { get; private set; }
    public PlayerState State { get; private set; }

    public PlayerContext
        (
        Rigidbody2D body,
        PlayerStats stats,
        PlayerInputHandler inputHandler,
        PlayerResources resources,
        PlayerUI ui,
        PlayerState state
        )
    {
        Body = body;
        Stats = stats;
        InputHandler = inputHandler;
        Resources = resources;
        UI = ui;
        State = state;
    }

    public void AddState(PlayerState s)
    {
        State |= s;
    }

    public void RemoveState(PlayerState s)
    {
        State &= ~s;
    }

    public bool HasAnyState(PlayerState flagsToCheck)
    {
        return (State & flagsToCheck) != 0;
    }
}
