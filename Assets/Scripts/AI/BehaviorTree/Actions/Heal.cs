using UnityEngine;

public class Heal : BehaviorTree
{
    private Transform healTarget; // Store the target from the query

    public override Result Run()
    {
        if (healTarget == null)  // Check if we have a target (set by the query)
        {
            return Result.FAILURE; // Or IN_PROGRESS, depending on your logic
        }

        EnemyAction act = agent.GetAction("heal");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(healTarget); // Use the stored target
        healTarget = null; // Reset the target after use
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public Heal() : base()
    {
    }

    public override BehaviorTree Copy()
    {
        return new Heal();
    }

    // This method will be called by the query to set the target
    public void SetHealTarget(Transform target)
    {
        healTarget = target;
    }
}