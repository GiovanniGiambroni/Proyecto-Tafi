using UnityEngine;

/// <summary>
/// Clase que representa los recursos del jugador. Esta clase expone los valores de vida, energía, stamina y cargas de membrana y permite su gestión.
/// </summary>
public class PlayerResources
{
    PlayerContext context;
    PlayerStats stats;

    public int Health { get ; private set; }
    public float Energy { get; private set; }
    public float Stamina { get; private set; }

    float staminaDelayTimer = 0;
    float staminaDelay = 0.1f;

    public PlayerResources(PlayerContext context)
    {
        this.context = context;
        stats = context.Stats;
    }

    public void Initialize()
    {
        Health = stats.MaxHealth;
        Energy = stats.MaxEnergy;
        Stamina = stats.MaxStamina;

        context.UI.StartStaminaSlider(Stamina);
    }

    /// <summary>
    /// Actualiza el ciclo de recursos del jugador.
    /// </summary>
    /// <param name="energy">¿Debe regenerar energía?</param>
    /// <param name="stamina">¿Debe regenerar stamina?</param>
    public void Update(bool regenEnergy, bool regenStamina)
    {
        if (regenEnergy) RegenerateEnergy();
        if (regenStamina) RegenerateStamina();

        context.UI.UpdateStamina(Stamina);
    }

    public void RegenerateEnergy()
    {
        Energy = Mathf.Min(Energy + stats.EnergyRegenRate * Time.deltaTime, stats.MaxEnergy);
    }

    public void RegenerateStamina()
    {
        if (staminaDelayTimer > 0)
        {
            staminaDelayTimer -= Time.deltaTime;
            return;
        }
        Stamina = Mathf.Min(Stamina + stats.StaminaRegenRate * Time.deltaTime, stats.MaxStamina);
    }

    public void ConsumeEnergy(float amount)
    {
        Energy = Mathf.Max(Energy - amount, 0);
    }

    public void ConsumeStamina(float amount)
    {
        Stamina = Mathf.Max(Stamina - amount, 0);
        staminaDelayTimer = staminaDelay;
    }

    public bool TryConsumeStamina(float amount)
    {
        if (Stamina >= amount)
        {
            ConsumeStamina(amount);
            return true;
        }
        return false;
    }
}
