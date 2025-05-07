using System.Collections.Generic;

public class Selector : InteriorNode
{
    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {
    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

    public override Result Run()
    {
        while (current_child < children.Count)
        {
            Result result = children[current_child].Run();

            if (result == Result.SUCCESS)
            {
                current_child = 0; // reset for next run
                return Result.SUCCESS;
            }

            if (result == Result.IN_PROGRESS)
            {
                return Result.IN_PROGRESS;
            }

            // If FAILURE, try next child
            current_child++;
        }

        // All children failed
        current_child = 0;
        return Result.FAILURE;
    }
}