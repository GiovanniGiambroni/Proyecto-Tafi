using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerStats stats;
    Rigidbody2D body;

    private Vector2 previousDir;

    Vector2 currentHeading = Vector2.up;
    float currentSpeed = 0;

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
    /*
    public void Move(Vector2 dir)
    {
        Vector2 targetVel = dir.normalized * stats.MoveSpeed;
        bool isStopping = dir.sqrMagnitude < 0.0001;

        float rate;
        if (isStopping) rate = stats.Deceleration;
        else
        {
            float alignment = body.linearVelocity.sqrMagnitude > 0.0001
                ? Vector2.Dot(body.linearVelocity.normalized, targetVel.normalized)
                : 1;

            float turnDiff = 1 - Mathf.InverseLerp(-1, 1, alignment);
            rate = Mathf.Lerp(stats.Acceleration, stats.Acceleration * stats.Maneuverability, turnDiff);
        }
        body.linearVelocity = Vector2.MoveTowards(body.linearVelocity, targetVel, rate * Time.deltaTime);
    }

    public void Move2(Vector2 dir)
    {
        bool hasInput = dir.sqrMagnitude > 0.0001;
        float speed = body.linearVelocity.magnitude;

        if (!hasInput)
        {
            body.linearVelocity = Vector2.MoveTowards(body.linearVelocity, Vector2.zero, stats.Deceleration * Time.deltaTime);
            return;
        }

        Vector2 targetDir = dir.normalized;
        Vector2 targetVel = targetDir * stats.MoveSpeed;

        float alignment = previousDir.sqrMagnitude > 0.0001
            ? Vector2.Dot(previousDir, targetDir)
            : 1;

        float turnDiff = 1 - Mathf.InverseLerp(-1, 1, alignment);

        float velAlignment = speed > 0.01
            ? Vector2.Dot(body.linearVelocity / speed, targetDir)
            : 1;

        if (velAlignment < -0.5) body.linearVelocity = Vector2.MoveTowards(body.linearVelocity, Vector2.zero, stats.Deceleration * Time.deltaTime);
        else
        {
            float rate = Mathf.Lerp(stats.Acceleration, stats.Acceleration * stats.Maneuverability, turnDiff);
            body.linearVelocity = Vector2.MoveTowards(body.linearVelocity, targetVel, rate * Time.deltaTime);
        }

        previousDir = targetDir;
    }

    public void Move3(Vector2 dir)
    {
        bool hasInput = dir.sqrMagnitude > 0.0001;
        Vector2 targetDir = hasInput ? dir.normalized : currentHeading;

        float targetSpeed = hasInput ? stats.MoveSpeed : 0;
        float speedRate = targetSpeed > currentSpeed ? stats.Acceleration : stats.Deceleration;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, speedRate * Time.deltaTime);

        if (!hasInput)
        {
            body.linearVelocity = currentHeading * currentSpeed;
            return;
        }

        if (currentSpeed < 0.05)
        {
            body.linearVelocity = targetDir * currentSpeed;
        }
        else
        {
            float maxDegrees = stats.MaxTurnSpeed * stats.Maneuverability * Time.deltaTime;
            currentHeading = RotateTowards(currentHeading, targetDir, maxDegrees);
            body.linearVelocity = currentHeading * currentSpeed;
        }
    }*/

    public void Move4(Vector2 dir)
    {
        Vector2 targetVel = dir.normalized * stats.MoveSpeed;

        bool isStopping = dir.sqrMagnitude > 0.001
            ? Vector2.Dot(previousDir, targetVel.normalized) < -0.86
            : true;

        float rate = isStopping ? stats.Deceleration : stats.Acceleration;
        body.linearVelocity = Vector2.MoveTowards(body.linearVelocity, targetVel, rate * Time.deltaTime);

        previousDir = targetVel.normalized;
    }

    private Vector2 RotateTowards(Vector2 from, Vector2 to, float maxDegrees)
    {
        float angle = Vector2.SignedAngle(from,to);
        float step = Mathf.Clamp(angle, -maxDegrees, maxDegrees);
        return Quaternion.Euler(0, 0, step) * from;
    }
}
