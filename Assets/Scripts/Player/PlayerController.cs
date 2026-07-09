using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStats stats;
    PlayerInputHandler inputHandler;
    PlayerActions actions;

    float MoveForce;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        inputHandler = GetComponent<PlayerInputHandler>();
        actions = GetComponent<PlayerActions>();
    }

    void FixedUpdate()
    {
        actions.Move4(inputHandler.MoveDir);
    }
}
