using UnityEngine;

public class FleeFromPlayer : BehaviorTree
{
    float fleeDistance;
    bool started = false;
    Vector3 fleeTarget;

    public FleeFromPlayer(float fleeDistance)
    {
        this.fleeDistance = fleeDistance;
    }

    public override Result Run()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
            return Result.FAILURE;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 selfPos = agent.transform.position;

        if (!started)
        {
            Vector3 awayDir = (selfPos - playerPos).normalized;
            fleeTarget = selfPos + awayDir * fleeDistance;
            started = true;
        }

        Vector3 direction = fleeTarget - agent.transform.position;
        if (direction.magnitude < 0.2f)
        {
            agent.GetComponent<Unit>().movement = Vector2.zero;
            started = false; // reset for next use
            return Result.SUCCESS;
        }

        agent.GetComponent<Unit>().movement = direction.normalized;
        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new FleeFromPlayer(fleeDistance);
    }
}
