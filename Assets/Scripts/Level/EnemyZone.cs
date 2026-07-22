using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public List<DoorController> Doors = new List<DoorController>();

    public void BeginZone()
    {
        foreach (var door in Doors)
        {
            door.Close();
        }
    }
}
