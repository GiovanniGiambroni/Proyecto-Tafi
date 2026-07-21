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
    Collider2D col;
    PlayerInputHandler inputHandler;
    PlayerUI ui;
    PlayerActions actions;
    PlayerState state = PlayerState.None;

    PlayerState NegateMovementStates = PlayerState.Stunned | PlayerState.Dashing;
    bool CanMove => !context.HasAnyState(NegateMovementStates);

    LayerMask dashLayerMask;

    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        ui = GetComponent<PlayerUI>();

        context = new(body, col, stats, inputHandler, resources, ui, state);
        actions = new(context);
        resources = new(context);
    }

    void Start()
    {
        resources.Initialize();
        dashLayerMask = LayerMask.GetMask("Wall", "Obstacle");
    }

    void FixedUpdate()
    {
        if (CanMove) actions.Move(inputHandler.MoveDir);
        resources.Update(false, true);
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
        if (!CanMove) return;
        if (!resources.TryConsumeStamina(stats.DashStaminaCost)) return;
        
        RaycastHit2D hit = Physics2D.Raycast(body.position, inputHandler.LastMoveInput, stats.DashDistance, dashLayerMask);
        if (hit) StartCoroutine(actions.Dash(inputHandler.LastMoveInput, hit));
        else StartCoroutine(actions.Dash(inputHandler.LastMoveInput));
    }
}