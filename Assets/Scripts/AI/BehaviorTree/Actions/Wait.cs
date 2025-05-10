using UnityEngine;

public class Wait : BehaviorTree
{
    float waitTime;
    float startTime;
    bool started;

    public override Result Run()
    {
        if (!started)
        {
            startTime = Time.time;
            started = true;
        }

        if (Time.time - startTime >= waitTime)
        {
            started = false; // reset for reuse
            return Result.SUCCESS;
        }

        return Result.IN_PROGRESS;
    }

    public Wait(float waitTime) : base()
    {
        this.waitTime = waitTime;
        this.started = false;
    }

    public override BehaviorTree Copy()
    {
        return new Wait(waitTime);
    }
}
