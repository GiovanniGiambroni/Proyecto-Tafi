using System.Collections.Generic;
using UnityEngine;

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
