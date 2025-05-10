using UnityEngine;

public class HealTargetNeedsHealingQuery : BehaviorTree
{
    private Transform targetToHeal; // Store the target to heal

    public override Result Run()
    {
        var target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);
        if (target == null) return Result.FAILURE;

        var controller = target.GetComponent<EnemyController>();
        if (controller == null || controller.monster != "zombie") return Result.FAILURE;

        if (controller.hp.hp < controller.hp.max_hp * 0.5f && controller.GetEffect("noheal") == 0)
        {
            targetToHeal = target.transform; // Store the target's transform
            return Result.SUCCESS;
        }

        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new HealTargetNeedsHealingQuery();
    }

    public Transform GetTargetToHeal()
    {
        return targetToHeal;
    }
}