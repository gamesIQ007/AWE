using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Enemy))]

/// <summary>
/// ИИ врагов
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Перечисление типов поведения
    /// </summary>
    public enum AIBehaviour
    {
        /// <summary>
        /// Ничего
        /// </summary>
        Null,
        /// <summary>
        /// Ожидание
        /// </summary>
        Idle,
        /// <summary>
        /// Патрулирование по маршруту
        /// </summary>
        Patrol,
        /// <summary>
        /// Преследование цели
        /// </summary>
        PursuitTarget
    }


    /// <summary>
    /// Поведение
    /// </summary>
    [SerializeField] private AIBehaviour aIBehaviour;
    public AIBehaviour Behaviour { get { return aIBehaviour; } set { aIBehaviour = value; } }

    /// <summary>
    /// Агент перемещения
    /// </summary>
    [SerializeField] private NavMeshAgent agent;

    /// <summary>
    /// Индекс точки маршрута патрулирования
    /// </summary>
    [SerializeField] private int patrolPathNodeIndex = 0;

    /// <summary>
    /// Путь перемещения
    /// </summary>
    private NavMeshPath navMeshPath;
    /// <summary>
    /// Текущая точка перемещения
    /// </summary>
    private PatrolPathNode currentPathNode;

    /// <summary>
    /// Маршрут патрулирования
    /// </summary>
    private PatrolPath patrolPath;

    /// <summary>
    /// Цель
    /// </summary>
    private GameObject target;

    /// <summary>
    /// Враг
    /// </summary>
    private Enemy enemy;


    private void Start()
    {
        enemy = GetComponent<Enemy>();

        FindMovementArea();

        navMeshPath = new NavMeshPath();
        StartBehaviour(aIBehaviour);
    }

    private void Update()
    {
        UpdateAI();

    }


    /// <summary>
    /// Обновление ИИ
    /// </summary>
    private void UpdateAI()
    {
        ActionUpdateTarget();

        if (aIBehaviour == AIBehaviour.Idle)
        {
            return;
        }

        if (aIBehaviour == AIBehaviour.PursuitTarget)
        {
            agent.CalculatePath(target.transform.position, navMeshPath);
            agent.SetPath(navMeshPath);

            /* переписать в атаку при приближении
            if (Vector3.Distance(transform.position, pursuitTarget.position) <= aimingDistance)
            {
                if (Vector3.Angle(pursuitTarget.position - transform.position, transform.forward) < 10)
                {
                    agent.isStopped = true;
                }
                alienSoldier.Fire(pursuitTarget.position + new Vector3(0, 1, 0));
            }*/
        }

        if (aIBehaviour == AIBehaviour.Patrol)
        {
            if (AgentReachedDestination())
            {
                StartCoroutine(SetBehaviourOnTime(AIBehaviour.Idle, currentPathNode.IdleTime));
            }
        }
    }


    /// <summary>
    /// Действие обновления цели
    /// </summary>
    private void ActionUpdateTarget()
    {
        if (target == null)
        {
            FindTarget();

            if (target == null) return;
        }

        if (/* засунуть условие проверки видна ли цель ||*/ Vector2.Distance(transform.position, target.transform.position) <= enemy.DetectDistance)
        {
            StartBehaviour(AIBehaviour.PursuitTarget);
        }
    }

    /// <summary>
    /// Найти цель
    /// </summary>
    private void FindTarget()
    {
        target = Player.Instance.gameObject;
    }


    #region Поведение

    /// <summary>
    /// Начать поведение
    /// </summary>
    /// <param name="behaviour">Поведение</param>
    private void StartBehaviour(AIBehaviour state)
    {
        if (enemy.IsDead) return;

        if (state == AIBehaviour.Idle)
        {
            agent.isStopped = true;
        }

        if (state == AIBehaviour.Patrol)
        {
            agent.isStopped = false;
            SetDestinationByPathNode(patrolPath.GetNextNode(ref patrolPathNodeIndex));
        }

        if (state == AIBehaviour.PursuitTarget)
        {
            agent.isStopped = false;
        }

        aIBehaviour = state;
    }

    /// <summary>
    /// Временно сменить поведение
    /// </summary>
    /// <param name="state">Поведение</param>
    /// <param name="second">Время</param>
    /// <returns></returns>
    IEnumerator SetBehaviourOnTime(AIBehaviour state, float second)
    {
        AIBehaviour previous = aIBehaviour;
        aIBehaviour = state;
        StartBehaviour(aIBehaviour);

        yield return new WaitForSeconds(second);

        StartBehaviour(previous);
    }

    #endregion


    /// <summary>
    /// Задать точку назначения из точки маршрута
    /// </summary>
    /// <param name="node">Точка маршрута</param>
    private void SetDestinationByPathNode(PatrolPathNode node)
    {
        currentPathNode = node;
        agent.CalculatePath(currentPathNode.transform.position, navMeshPath);
        agent.SetPath(navMeshPath);
    }

    /// <summary>
    /// Агент достиг точки назначения
    /// </summary>
    /// <returns>Агент достиг точки назначения</returns>
    private bool AgentReachedDestination()
    {
        if (agent.pathPending == false)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (agent.hasPath == false || agent.velocity.sqrMagnitude == 0.0f)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    /// <summary>
    /// Найти маршрут перемещения
    /// </summary>
    private void FindMovementArea()
    {
        if (patrolPath == null)
        {
            PatrolPath[] patrolPaths = FindObjectsOfType<PatrolPath>();

            float minDistance = float.MaxValue;

            for (int i = 0; i < patrolPaths.Length; i++)
            {
                if (Vector3.Distance(transform.position, patrolPaths[i].transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(transform.position, patrolPaths[i].transform.position);
                    patrolPath = patrolPaths[i];
                }
            }
        }
    }
}
