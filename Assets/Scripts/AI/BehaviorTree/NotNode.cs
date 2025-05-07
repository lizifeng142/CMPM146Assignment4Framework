using System.Collections.Generic;

public class NotNode : BehaviorTree
{
    private BehaviorTree child;

    public NotNode(BehaviorTree child)
    {
        this.child = child;
    }

    public override Result Run()
   {
        var result = child.Run();
        if (result == Result.SUCCESS) return Result.FAILURE;
        if (result == Result.FAILURE) return Result.SUCCESS;
        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new NotNode(child.Copy());
    }

    public override IEnumerable<BehaviorTree> AllNodes()
    {
        yield return this;
        foreach (var node in child.AllNodes())
        {
            yield return node;
        }
    }
}