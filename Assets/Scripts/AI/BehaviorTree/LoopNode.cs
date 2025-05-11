using System.Collections.Generic;
using UnityEngine;

public class LoopNode : BehaviorTree
{
    private BehaviorTree child;

    public LoopNode(BehaviorTree child)
    {
        this.child = child;
    }

    public override Result Run()
    {
        child.Run(); 
        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new LoopNode(child.Copy());
    }

    public override IEnumerable<BehaviorTree> AllNodes()
    {
        yield return this;
        foreach (var node in child.AllNodes())
            yield return node;
    }
}