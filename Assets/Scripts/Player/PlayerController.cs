using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerContext context;

    [SerializeField] PlayerStats stats = new();
    PlayerResources resources;
    Rigidbody2D body;
    PlayerInputHandler inputHandler;
    PlayerActions actions;
    PlayerState state = PlayerState.None;

    PlayerState NegateMovementStates = PlayerState.Stunned | PlayerState.Dashing;
    bool CanMoveOrDash => !context.HasAnyState(NegateMovementStates);

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
        if (CanMoveOrDash) actions.Move(inputHandler.MoveDir);
    }

    void OnEnable()
    {
        SubscribeActions();
    }

    void OnDisable()
    {
        UnsubscribeActions();
    }

    void SubscribeActions()
    {
        inputHandler.DashEvent += Dash;
    }

    void UnsubscribeActions()
    {
        inputHandler.DashEvent -= Dash;
    }

    void Dash()
    {
        if (CanMoveOrDash) StartCoroutine(actions.Dash(inputHandler.MoveDir));
    }
}