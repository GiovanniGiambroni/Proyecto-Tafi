using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gestiona el comportamiento del jugador. Se encarga de validar acciones y otros procesos relacionados.
/// </summary>
public class PlayerController : MonoBehaviour
{
    PlayerContext context;

    [SerializeField] PlayerStats stats = new();
    PlayerResources resources;
    Rigidbody2D body;
    PlayerInputHandler inputHandler;
    PlayerUI ui;
    PlayerActions actions;
    PlayerState state = PlayerState.None;

    PlayerState NegateMovementStates = PlayerState.Stunned | PlayerState.Dashing;
    bool CanMove => !context.HasAnyState(NegateMovementStates);

    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        body = GetComponent<Rigidbody2D>();
        ui = GetComponent<PlayerUI>();

        context = new(body, stats, inputHandler, resources, ui, state);
        actions = new(context);
        resources = new(context);
    }

    void FixedUpdate()
    {
        if (CanMove) actions.Move(inputHandler.MoveDir);
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
        if (CanMove) StartCoroutine(actions.Dash(inputHandler.MoveDir));
    }
}