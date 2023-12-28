using System.Collections;
using UnityEngine;


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
    /// Индекс точки маршрута патрулирования
    /// </summary>
    [SerializeField] private int patrolPathNodeIndex = 0;

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

        StartBehaviour(aIBehaviour);

        enemy.ChangeHitPoints.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        enemy.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void Update()
    {
        if (aIBehaviour == AIBehaviour.Null)
        {
            return;
        }

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
            enemy.MoveTo(target);

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= enemy.MeleeAttackDistance)
            {
                enemy.AttackMeleeWeapon(target.transform.position);
            }
            if (enemy.Type == EnemyType.Shooter && distanceToTarget > enemy.MeleeAttackDistance && distanceToTarget <= enemy.ShootAttackDistance)
            {
                enemy.AttackDistanceWeapon(target.transform.position);
            }
        }

        if (aIBehaviour == AIBehaviour.Patrol)
        {
            enemy.MoveTo(currentPathNode.gameObject);

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
        
        if (Vector2.Distance(transform.position, target.transform.position) <= enemy.DetectDistance)
        {
            /* засунуть условие проверки видна ли цель ||*/
            //RaycastHit hit;

            /*Debug.DrawLine(point, points[i], Color.blue);
            if (Physics.Raycast(point, (points[i] - point).normalized, out hit, viewDistance * 2))
            {
                if (hit.collider == collider)
                {
                    return true;
                }
            }*/
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

        if (state == AIBehaviour.Patrol)
        {
            SetDestinationByPathNode(patrolPath.GetNextNode(ref patrolPathNodeIndex));
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
    }

    /// <summary>
    /// Агент достиг точки назначения
    /// </summary>
    /// <returns>Агент достиг точки назначения</returns>
    private bool AgentReachedDestination()
    {
        if (Vector2.Distance(transform.position, currentPathNode.transform.position) <= 0.5f)
        {
            return true;
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

    /// <summary>
    /// Действие при получении урона
    /// </summary>
    private void OnChangeHitPoints(int damage, Vector2 position)
    {
        if (enemy.MaxHitPoints == enemy.HitPoints) return;
        
        StartBehaviour(AIBehaviour.PursuitTarget);
    }
}
