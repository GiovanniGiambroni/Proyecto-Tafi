using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction dashAction;

    public Vector2 MoveDir { get; private set; } = new();
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
