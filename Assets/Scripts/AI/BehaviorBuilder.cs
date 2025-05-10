using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        EnemyAction attackAction = agent.GetAction("attack");
        float range = (attackAction != null) ? attackAction.range : 1.5f;

        if (agent.monster == "warlock")
        {
            var groupPoint = AIWaypointManager.Instance.GetClosestByPrefix(agent.transform.position, "Group");

            if (groupPoint == null)
            {
                Debug.LogWarning("No group waypoint found for warlock.");
                result = new Wait(1.0f); // fallback to idle instead of attacking
            }
            else
            {
                result = new Selector(new BehaviorTree[] {
                    // Flee if player is close
                    new Sequence(new BehaviorTree[] {
                        new PlayerTooCloseQuery(12.0f),
                        new FleeFromPlayer(3.0f)
                    }),

                    // Wait for group composition before moving in
                    new Sequence(new BehaviorTree[] {
                        new NearbyEnemiesQuery(2, 6f), // wait for at least 2 other enemies nearby (warlock count check)
                        new GoTo(groupPoint.transform, 1.5f),
                        new NearbyEnemiesQuery(6, 6f), // total enemies nearby (zombies + skeletons + warlocks)
                        new Selector(new BehaviorTree[] {
                            new Sequence(new BehaviorTree[] {
                                new AbilityReadyQuery("permabuff"),
                                new IsNearestAllyTypeQuery("skeleton"),
                                new PermaBuff()
                            }),
                            new Sequence(new BehaviorTree[] {
                                new AbilityReadyQuery("buff"),
                                new IsNearestAllyTypeQuery("skeleton"),
                                new Buff()
                            }),
                            new Sequence(new BehaviorTree[] {
                                new AbilityReadyQuery("heal"),
                                new HealTargetNeedsHealingQuery(), // NEW: Check if a Zombie needs healing
                                new RunHealAction() // Custom action to run Heal
                            }),
                            new Wait(1.0f)
                        })
                    }),

                    new Wait(1.0f) // idle fallback
                });
            }
        }
        else if (agent.monster == "zombie")
        {
            var groupPoint = AIWaypointManager.Instance.GetClosestByPrefix(agent.transform.position, "Group");
            if (groupPoint == null)
            {
                Debug.LogWarning("No group waypoint found. Falling back to default attack behavior.");
                result = new Sequence(new BehaviorTree[] {
                    new MoveToPlayer(range),
                    new Attack()
                });
            }
            else
            {
                result = new Sequence(new BehaviorTree[] {
                    new GoTo(groupPoint.transform, 1.5f),
                    new NearbyEnemiesQuery(6, 6f), // wait for group before attack
                    new MoveToPlayer(range),
                    new Attack()
                });
            }
        }
        else if (agent.monster == "skeleton")
        {
            var groupPoint = AIWaypointManager.Instance.GetClosestByPrefix(agent.transform.position, "Group");
            if (groupPoint == null)
            {
                Debug.LogWarning("No group waypoint found. Falling back to default skeleton attack behavior.");
                result = new Sequence(new BehaviorTree[] {
                    new MoveToPlayer(range),
                    new Attack()
                });
            }
            else
            {
                result = new Selector(new BehaviorTree[] {
                    new Sequence(new BehaviorTree[] {
                        new PlayerTooCloseQuery(5.0f),
                        new NotNode(new ZombiesNearPlayerQuery(2, 6.0f)),
                        new GoTo(groupPoint.transform, 1.5f),
                        new Wait(2.0f)
                    }),
                    new Sequence(new BehaviorTree[] {
                        new GoTo(groupPoint.transform, 1.5f),
                        new NearbyEnemiesQuery(6, 6f), // wait for group
                        new MoveToPlayer(range),
                        new Attack()
                    })
                });
            }
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                new MoveToPlayer(range),
                new Attack()
            });
        }

        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }

        return result;
    }
}