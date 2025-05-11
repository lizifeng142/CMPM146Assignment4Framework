using UnityEngine;
using System.Collections.Generic;

public class NearbyEnemiesQuery : BehaviorTree
{
    int count;
    float distance;

    public NearbyEnemiesQuery(int count, float distance) : base()
    {
        this.count = count;
        this.distance = distance;
    }

    public override Result Run()
    {
        if (agent == null)
        {
            Debug.LogWarning("NearbyEnemiesQuery: agent is null.");
            return Result.FAILURE;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("NearbyEnemiesQuery: GameManager.Instance is null.");
            return Result.FAILURE;
        }

        if (agent.transform == null)
        {
            Debug.LogWarning("NearbyEnemiesQuery: agent.transform is null.");
            return Result.FAILURE;
        }

        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        if (nearby == null)
        {
            Debug.LogWarning("NearbyEnemiesQuery: GetEnemiesInRange returned null.");
            return Result.FAILURE;
        }

        if (nearby.Count >= count)
        {
            Debug.Log("NearbyEnemiesQuery: Found " + nearby.Count + " nearby enemies. Triggering global charge.");
            GroupChargeManager.TriggerGlobalCharge();
            return Result.FAILURE;
        }

        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyEnemiesQuery(count, distance);
    }
}