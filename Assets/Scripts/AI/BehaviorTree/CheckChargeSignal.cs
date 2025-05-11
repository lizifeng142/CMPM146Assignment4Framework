using UnityEngine;

public class CheckChargeSignal : BehaviorTree
{
    public override Result Run()
    {
        if (GroupChargeManager.shouldCharge)
        {
            return Result.SUCCESS;
        }

        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new CheckChargeSignal();
    }
}