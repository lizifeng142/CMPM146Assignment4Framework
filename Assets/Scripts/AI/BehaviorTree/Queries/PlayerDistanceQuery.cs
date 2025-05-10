using UnityEngine;

public class PlayerDistanceQuery : BehaviorTree
{
    float minDistance;

    public override Result Run()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
            return Result.FAILURE;

        float dist = Vector3.Distance(agent.transform.position, GameManager.Instance.player.transform.position);

        if (dist >= minDistance)
            return Result.SUCCESS;
        else
            return Result.FAILURE;
    }

    public PlayerDistanceQuery(float minDistance) : base()
    {
        this.minDistance = minDistance;
    }

    public override BehaviorTree Copy()
    {
        return new PlayerDistanceQuery(minDistance);
    }
}
