using UnityEngine;

public class RunHealAction : BehaviorTree
{
    public override Result Run()
    {
        HealTargetNeedsHealingQuery healTargetQuery = (HealTargetNeedsHealingQuery)GetChildOfType<HealTargetNeedsHealingQuery>(this);
        Heal healAction = (Heal)GetChildOfType<Heal>(this);

        if (healTargetQuery == null || healAction == null)
        {
            return Result.FAILURE; // Or handle the error appropriately
        }

        Transform targetToHeal = healTargetQuery.GetTargetToHeal();

        if (targetToHeal == null)
        {
            return Result.FAILURE; // No target to heal
        }

        healAction.SetHealTarget(targetToHeal); // Set the target in the Heal action
        return healAction.Run(); // Run the Heal action
    }

    private BehaviorTree GetChildOfType<T>(BehaviorTree node) where T : BehaviorTree
    {
        if (node is T && node.agent == agent)
        {
            return node;
        }

        if (node is InteriorNode interiorNode)
        {
            foreach (var child in interiorNode.children)
            {
                var found = GetChildOfType<T>(child);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }

    public override BehaviorTree Copy()
    {
        return new RunHealAction();
    }
}