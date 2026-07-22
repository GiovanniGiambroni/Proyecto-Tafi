using UnityEngine;

/// <summary>
/// Logica de activación de la zona de enemigos.
/// </summary>
public class ZoneTrigger : MonoBehaviour
{
    public EnemyZone EnemyZone;
    Collider2D trigger;

    void Awake()
    {
        trigger = GetComponent<Collider2D>();

        EnemyZone = GetComponentInParent<EnemyZone>();
        if (EnemyZone == null)
        {
            Debug.LogError("EnemyZone reference is not set or failed to do so.");
        }
    }

    // Cuando el jugador entra en el trigger, se inicia la zona de enemigos y se desactiva el trigger para evitar múltiples activaciones.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyZone.BeginZone(collision.gameObject);
            trigger.enabled = false;
        }
    }

    public void ResetTrigger()
    {
        trigger.enabled = true;
    }
}
