using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una zona de enemigos en el juego. Contiene puertas, un trigger para iniciar la zona y varias oleadas de enemigos.
/// </summary>
/// <remarks>
/// La clase <see cref="EnemyZone"/> gestiona la activación de oleadas de enemigos y el control de las puertas asociadas a la zona. Cuando el jugador entra en la zona, las puertas se cierran y se activa la primera oleada de enemigos. Una vez que todas las oleadas han sido completadas, las puertas se abren nuevamente.
/// </remarks>
public class EnemyZone : MonoBehaviour
{
    public List<DoorController> Doors = new List<DoorController>();
    public ZoneTrigger Trigger;
    public List<EnemyWave> EnemyWaves = new List<EnemyWave>();

    int currentWaveIndex = 0;
    PlayerContext player;

    public void BeginZone(GameObject player)
    {
        try
        {
            this.player = player.GetComponent<PlayerController>().Context;
        }
        catch
        {
            Debug.LogError("Unable to get PlayerContext");
            return;
        }

        SetDoors(false);

        EnemyWaves[0].Activate(this.player);
    }

    public void OnWaveCompleted()
    {
        currentWaveIndex++;
        if (currentWaveIndex < EnemyWaves.Count)
        {
            EnemyWaves[currentWaveIndex].Activate(player);
        }
        else
        {
            SetDoors(true);
        }
    }

    public void ResetZone()
    {
        foreach (var wave in EnemyWaves)
        {
            wave.ResetWave();
        }
        SetDoors(true);
        Trigger.ResetTrigger();
    }

    /// <summary>
    /// Cambia el estado de las puertas del EnemyZone.
    /// </summary>
    /// <param name="open">True para abiertas, False para cerradas</param>
    void SetDoors(bool open)
    {
        foreach (var door in Doors)
        {
            if (open) door.Open();
            else door.Close();
        }
    }
}
