using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public EnemyZone EnemyZone;
    Collider2D trigger;

    private void Awake()
    {
        trigger = GetComponent<Collider2D>();

        EnemyZone = GetComponentInParent<EnemyZone>();
        if (EnemyZone == null)
        {
            Debug.LogError("EnemyZone reference is not set or failed to do so.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyZone.BeginZone();
            trigger.enabled = false;
        }
    }
}
