using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions
{
    PlayerContext context;

    private Rigidbody2D rb;
    private PlayerStats stats;

    private Vector2 previousDir;

    public PlayerActions(PlayerContext context)
    {
        this.context = context;
        rb = context.Body;
        stats = context.Stats;
        Debug.Log("Is reference equal: " + ReferenceEquals(rb, context.Body));
    }

    public void Move(Vector2 dir)
    {
        Vector2 targetVel = dir.normalized * stats.MoveSpeed;

        bool isStopping = dir.sqrMagnitude <= 0.001 || Vector2.Dot(previousDir, targetVel.normalized) < -0.86;
        float rate = isStopping ? stats.Deceleration : stats.Acceleration;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVel, rate * Time.deltaTime);

        previousDir = targetVel.normalized;
    }
}
