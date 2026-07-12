using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Provee métodos representativos de las acciones del jugador. Sintetiza física, manejo de estados y estadísticas para que otras clases puedan invocar acciones del jugador sin preocuparse por los detalles de implementación.
/// </summary>
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
    }

    public void Move(Vector2 dir)
    {
        context.AddState(PlayerState.Moving);

        Vector2 targetVel = dir.normalized * stats.MoveSpeed;

        bool isStopping = dir.sqrMagnitude <= 0.001 || Vector2.Dot(previousDir, targetVel.normalized) < -0.86;
        float rate = isStopping ? stats.Deceleration : stats.Acceleration;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVel, rate * Time.deltaTime);

        previousDir = targetVel.normalized;
    }

    public IEnumerator Dash(Vector2 dir)
    {
        context.AddState(PlayerState.Dashing);

        try
        {
            Vector2 dashDir = dir != Vector2.zero
            ? dir.normalized
            : previousDir;
            float dashSpeed = stats.DashDistance / stats.DashDuration;

            // Reseteo velocidad para evitar que se desvíe de la trayectoria del dash
            rb.linearVelocity = Vector2.zero;

            float elapsed = 0;
            while (elapsed < stats.DashDuration)
            {
                rb.position = Vector2.MoveTowards(rb.position, rb.position + dashDir * stats.DashDistance, dashSpeed * Time.deltaTime);

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Le devuelvo la velocidad pero acelerada en dirección del dash, para dar la sensación correcta de movimiento
            rb.linearVelocity = dashDir * stats.MoveSpeed;
        }
        finally
        {
            context.RemoveState(PlayerState.Dashing);
        }
    }
}
