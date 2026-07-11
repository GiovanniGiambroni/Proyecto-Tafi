using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerContext context;

    [SerializeField] PlayerStats stats = new();
    PlayerResources resources;
    Rigidbody2D body;
    PlayerInputHandler inputHandler;
    PlayerState state = PlayerState.None;

    PlayerActions actions;

    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        body = GetComponent<Rigidbody2D>();

        context = new(body, stats, inputHandler, resources, state);
        actions = new(context);
        resources = new(context);
    }

    void FixedUpdate()
    {
        actions.Move(inputHandler.MoveDir);
    }
}
