using System.Collections;
using UnityEngine;

/// <summary>
/// Script de seguimiento de la cámara.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Rigidbody2D target;
    public Vector3 offset;

    public float outerMarginRadius = 0.5f;
    public float innerMarginRadius = 0.1f;

    public float smoothTime = 0.15f;

    // Internal velocity used by SmoothDamp.
    private Vector3 velocity = Vector3.zero;

    Vector3 targetPos;
    bool Reached => Vector2.Distance(transform.position, targetPos) <= innerMarginRadius;
    bool InRange => Vector2.Distance(transform.position, targetPos) < outerMarginRadius;
    bool wasMoving = false;

    void LateUpdate()
    {
        if (target == null) return;

        targetPos = target.transform.position + offset;
        Debug.DrawLine(transform.position, targetPos, Color.red);

        if (Reached)
        {
            wasMoving = false;
            return;
        }

        if (!wasMoving && InRange) return;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        wasMoving = true;
    }
}
