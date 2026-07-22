using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool StartOpen = true;

    SpriteRenderer spriteRenderer;
    Collider2D doorCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        if (StartOpen) Open();
        else Close();
    }

    public void Open()
    {
        spriteRenderer.enabled = false;
        doorCollider.enabled = false;
    }

    public void Close()
    {
        spriteRenderer.enabled = true;
        doorCollider.enabled = true;
    }
}
