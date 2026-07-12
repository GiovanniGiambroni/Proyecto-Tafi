using UnityEngine;

/// <summary>
/// Clase que representa los recursos del jugador. Esta clase expone los valores de vida, energía, stamina y cargas de membrana y permite su gestión.
/// </summary>
public class PlayerResources
{
    PlayerContext context;
    public PlayerResources(PlayerContext context)
    {
        this.context = context;
    }
}
