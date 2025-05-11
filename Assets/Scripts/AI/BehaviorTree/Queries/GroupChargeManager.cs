using UnityEngine;
using System.Collections.Generic;

public static class GroupChargeManager
{
    public static bool shouldCharge = false;
    private static List<GameObject> trackedEnemies = new List<GameObject>();
    private static int threshold = 7;

    public static void TriggerGlobalCharge()
    {
        shouldCharge = true;
    }

    public static void ResetCharge()
    {
        shouldCharge = false;
    }

    public static void RegisterEnemy(GameObject enemy)
    {
        if (!trackedEnemies.Contains(enemy))
            trackedEnemies.Add(enemy);
    }

    public static void UnregisterEnemy(GameObject enemy)
    {
        trackedEnemies.Remove(enemy);

        if (shouldCharge && trackedEnemies.Count < threshold)
        {
            Debug.Log("Resetting global charge — enemy count dropped below threshold.");
            shouldCharge = false;
        }
    }
    public static List<GameObject> GetTrackedEnemies()
    {
        return trackedEnemies;
    }
}