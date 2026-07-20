using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Maneja la entrada del jugador. Esta clase se encarga de leer las entradas del jugador y exponerlas a otras clases a través de eventos y propiedades.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction dashAction;

    public Vector2 MoveDir { get; private set; } = new();
    public Vector2 LastMoveInput { get; private set; } = new();
    public Action DashEvent { get; set; }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move");
        dashAction = playerInput.actions.FindAction("Dash");

        SubscribeActions();
    }

    void Start()
    {

    }

    void Update()
    {
        MoveDir = moveAction.ReadValue<Vector2>();
        if (MoveDir != Vector2.zero)
        {
            LastMoveInput = MoveDir;
        }
    }

    void OnDestroy()
    {
        UnsubscribeActions();
    }

    void SubscribeActions()
    {
        dashAction.performed += OnDash;
    }

    void UnsubscribeActions()
    {
        dashAction.performed -= OnDash;
    }

    void OnDash(InputAction.CallbackContext c)
    {
        DashEvent?.Invoke();
    }
}
