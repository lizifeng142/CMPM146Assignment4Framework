using UnityEngine;
using System.Collections.Generic;

public class ZombiesNearPlayerQuery : BehaviorTree
{
    int requiredCount;
    float radius;

    public override Result Run()
    {
        List<GameObject> enemies = GameManager.Instance.GetEnemiesInRange(
            GameManager.Instance.player.transform.position,
            radius
        );

        int zombieCount = 0;
        foreach (GameObject enemy in enemies)
        {
            var controller = enemy.GetComponent<EnemyController>();
            if (controller != null && controller.monster == "zombie")
            {
                zombieCount++;
                if (zombieCount >= requiredCount)
                    return Result.SUCCESS;
            }
        }

        return Result.FAILURE;
    }

    public ZombiesNearPlayerQuery(int requiredCount, float radius) : base()
    {
        this.requiredCount = requiredCount;
        this.radius = radius;
    }

    public override BehaviorTree Copy()
    {
        return new ZombiesNearPlayerQuery(requiredCount, radius);
    }
}
