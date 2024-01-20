using UnityEngine;


/// <summary>
/// ИИ босса
/// </summary>
public class BossAI : EnemyAI
{
    /// <summary>
    /// Босс
    /// </summary>
    private Boss boss;


    protected override void Start()
    {
        enemy = GetComponent<Enemy>();

        FindMovementArea();

        StartBehaviour(aIBehaviour);

        boss = GetComponent<Boss>();
    }

    protected override void OnDestroy()
    {
        
    }


    /// <summary>
    /// Обновление ИИ
    /// </summary>
    protected override void UpdateAI()
    {
        ActionUpdateTarget();

        if (aIBehaviour == AIBehaviour.Idle)
        {
            return;
        }

        if (aIBehaviour == AIBehaviour.Patrol)
        {
            enemy.MoveTo(currentPathNode.gameObject);

            if (AgentReachedDestination())
            {
                StartCoroutine(SetBehaviourOnTime(AIBehaviour.Idle, currentPathNode.IdleTime));
            }

            boss.AttackDistanceWeapon(target.transform.position);
        }
    }

    /// <summary>
    /// Действие обновления цели
    /// </summary>
    protected override void ActionUpdateTarget()
    {
        if (target == null)
        {
            FindTarget();

            if (target == null) return;
        }

        for (int i = 0; i < boss.Weapons.Length; i++)
        {
            boss.Weapons[i].SetTarget(target);
        }
    }
}
