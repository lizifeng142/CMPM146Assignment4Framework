using UnityEngine;

public class PlayerTooCloseQuery : BehaviorTree
{
    float maxDistance;

    public override Result Run()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
            return Result.FAILURE;

        float dist = Vector3.Distance(agent.transform.position, GameManager.Instance.player.transform.position);
        return (dist <= maxDistance) ? Result.SUCCESS : Result.FAILURE;
    }

    public PlayerTooCloseQuery(float maxDistance) : base()
    {
        this.maxDistance = maxDistance;
    }

    public override BehaviorTree Copy()
    {
        return new PlayerTooCloseQuery(maxDistance);
    }
}
