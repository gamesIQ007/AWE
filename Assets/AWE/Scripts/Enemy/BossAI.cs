using System;
using UnityEngine;


/// <summary>
/// ИИ босса
/// </summary>
public class BossAI : EnemyAI
{
    /// <summary>
    /// Набор для атаки. Активируется на время.
    /// </summary>
    [Serializable]
    private class AttackPack
    {
        /// <summary>
        /// Атакующий объект
        /// </summary>
        public GameObject[] AttackObjects;
        /// <summary>
        /// Время, на которое происходит активация
        /// </summary>
        public float Time;
    }


    /// <summary>
    /// Наборы атак
    /// </summary>
    [SerializeField] private AttackPack[] attackPacks;

    /// <summary>
    /// Индекс активного набора атак
    /// </summary>
    private int enabledPackAttackIndex = -1;

    /// <summary>
    /// Таймер набора атак
    /// </summary>
    private float attackPackTimer = 0;

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

            //boss.AttackDistanceWeapon(target.transform.position);

            UpdateAttackPacks();
        }
    }

    /// <summary>
    /// Обновление действий наборов атак
    /// </summary>
    private void UpdateAttackPacks()
    {
        attackPackTimer -= Time.deltaTime;

        if (attackPackTimer < 0)
        {
            if (enabledPackAttackIndex >= 0)
            {
                for (int i = 0; i < attackPacks[enabledPackAttackIndex].AttackObjects.Length; i++)
                {
                    attackPacks[enabledPackAttackIndex].AttackObjects[i].SetActive(false);
                }
            }

            enabledPackAttackIndex++;
            if (enabledPackAttackIndex == attackPacks.Length)
            {
                enabledPackAttackIndex = 0;
            }

            attackPackTimer = attackPacks[enabledPackAttackIndex].Time;

            for (int i = 0; i < attackPacks[enabledPackAttackIndex].AttackObjects.Length; i++)
            {
                attackPacks[enabledPackAttackIndex].AttackObjects[i].SetActive(true);
            }
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
