using UnityEngine;

public class PlayerContext
{
    public Rigidbody2D Body {  get; private set; }
    public PlayerStats Stats { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerResources Resources { get; private set; }
    public PlayerState State { get; private set; }

    public PlayerContext
        (
        Rigidbody2D body,
        PlayerStats stats,
        PlayerInputHandler inputHandler,
        PlayerResources resources,
        PlayerState state
        )
    {
        Body = body;
        Stats = stats;
        InputHandler = inputHandler;
        Resources = resources;
        State = state;
    }
}
