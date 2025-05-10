using UnityEngine;

public class IsNearestAllyTypeQuery : BehaviorTree
{
    string type;

    public override Result Run()
    {
        var target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);
        if (target == null) return Result.FAILURE;

        var controller = target.GetComponent<EnemyController>();
        if (controller != null && controller.monster == type)
            return Result.SUCCESS;

        return Result.FAILURE;
    }

    public IsNearestAllyTypeQuery(string type) : base()
    {
        this.type = type;
    }

    public override BehaviorTree Copy()
    {
        return new IsNearestAllyTypeQuery(type);
    }
}
