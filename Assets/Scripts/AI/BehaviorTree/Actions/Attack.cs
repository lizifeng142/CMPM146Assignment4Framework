using UnityEngine;

public class Attack : BehaviorTree
{
    public override Result Run()
    {
        EnemyAction act = agent.GetAction("attack");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(GameManager.Instance.player.transform);

        // Stay active while attacking
        return success ? Result.IN_PROGRESS : Result.FAILURE;
    }

    public Attack() : base() { }

    public override BehaviorTree Copy()
    {
        return new Attack();
    }
}