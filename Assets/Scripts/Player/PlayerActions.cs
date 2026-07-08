using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerStats stats;
    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move(Vector2 dir)
    {
        float moveRate;
        Vector2 currentVelocity = body.linearVelocity;
        Vector2 targetVelocity = dir * stats.Speed;

        // Si el jugador no se mueve, freno más rápido
        if (dir.magnitude == 0) moveRate = stats.StopRate;

        // Si apenas comiensza el movimiento, simplemente acelero
        else if (currentVelocity.sqrMagnitude < 0.01f) moveRate = stats.Acceleration;

        // Para el resto del movimiento, uso factor de giro
        else
        {
            float angle = Vector2.Angle(currentVelocity, targetVelocity);
            float turnFactor = angle / 180;
            moveRate = Mathf.Lerp(stats.Acceleration, stats.TurnSpeed, turnFactor);
        }

        body.linearVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, moveRate * Time.fixedDeltaTime);
    }
}
