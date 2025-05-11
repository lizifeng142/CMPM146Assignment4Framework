using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;
        var groupPoint = AIWaypointManager.Instance.GetClosestByPrefix(agent.transform.position, "Group");

        float attackRange = agent.GetAction("attack")?.range ?? 1.5f;

        // Default attack behavior
        BehaviorTree attackSequence = new Sequence(new BehaviorTree[] {
            new MoveToPlayer(attackRange),
            new Attack()
        });

        // Warlock support behavior (NO attacking)
        BehaviorTree warlockSupportSequence = new Sequence(new BehaviorTree[] {
            new FollowAlly(2.0f),
            new Heal(),
            new PermaBuff(),
            new Buff()
        });

        // Grouping phase (null-safe: if no groupPoint, skip grouping)
        BehaviorTree groupSequence = (groupPoint != null)
            ? new Sequence(new BehaviorTree[] {
                new GoTo(groupPoint.transform, 5.0f),
                new NearbyEnemiesQuery(12, 30.0f)
            })
            : null;

        // Group-based monsters
        if (agent.monster == "warlock" || agent.monster == "skeleton" || agent.monster == "zombie")
        {
            BehaviorTree postChargeBehavior = agent.monster == "warlock"
                ? warlockSupportSequence
                : attackSequence;

            var loopingPostCharge = new LoopNode(
                new Sequence(new BehaviorTree[] {
                    new CheckChargeSignal(),
                    postChargeBehavior
                })
            );

            if (groupSequence != null)
            {
                result = new Sequence(new BehaviorTree[] {
                    new Selector(new BehaviorTree[] {
                        new CheckChargeSignal(),
                        groupSequence
                    }),
                    loopingPostCharge
                });
            }
            else
            {
                // No group point, skip to fallback
                result = loopingPostCharge;
            }
        }
        else
        {
            result = new LoopNode(attackSequence);
        }

        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }

        return result;
    }
}