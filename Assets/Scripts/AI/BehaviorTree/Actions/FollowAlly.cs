using UnityEngine;

public class FollowAlly : BehaviorTree
{
    float followRange;
    GameObject target;

    public FollowAlly(float range)
    {
        followRange = range;
    }

    public override Result Run()
    {
        target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);

        if (target == null)
            return Result.FAILURE;

        Vector3 direction = target.transform.position - agent.transform.position;
        if (direction.magnitude < followRange)
        {
            agent.GetComponent<Unit>().movement = Vector2.zero;
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public override BehaviorTree Copy()
    {
        return new FollowAlly(followRange);
    }
}