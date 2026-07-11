using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;

    public Vector2 MoveDir { get; private set; } = new();

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move");
    }

    void Start()
    {

    }

    void Update()
    {
        MoveDir = moveAction.ReadValue<Vector2>();
    }
}
