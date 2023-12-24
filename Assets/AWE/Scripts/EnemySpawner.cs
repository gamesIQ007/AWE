using System;
using UnityEngine;


/// <summary>
/// Спавнер врагов
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Типа запуска следующего спавна
    /// </summary>
    public enum SpawnType
    {
        /// <summary>
        /// Спавн по времени
        /// </summary>
        SpawnByTime,
        /// <summary>
        /// Спавн по смерти всех врагов
        /// </summary>
        SpawnByDeath
    }

    /// <summary>
    /// Класс сквада врагов
    /// </summary>
    [Serializable]
    private class Squad
    {
        /// <summary>
        /// Префаб врага
        /// </summary>
        public Enemy EnemyPrefab;
        /// <summary>
        /// Количество
        /// </summary>
        public int Count;
        /// <summary>
        /// Область спавна
        /// </summary>
        public CircleArea SpawnArea;
        /// <summary>
        /// Тип запуска следующего спавна
        /// </summary>
        public SpawnType SpawnType;
        /// <summary>
        /// Время, через которое спавн будет завершён и запущен следующий спавн
        /// </summary>
        public float SpawnTime;
    }

    /// <summary>
    /// Сквады врагов
    /// </summary>
    [SerializeField] private Squad[] squads;

    /// <summary>
    /// Индекс текущего сквада врагов
    /// </summary>
    private int currentSquad = -1;

    /// <summary>
    /// Время текущего спавна
    /// </summary>
    private float currentSpawnTime = 0;

    /// <summary>
    /// Количество живых врагов
    /// </summary>
    private int enemyCount = 0;

    /// <summary>
    /// Готов спавнить следующего
    /// </summary>
    private bool readyToNextSpawn = false;

    /// <summary>
    /// Готов к работе
    /// </summary>
    private bool readyToWork = false;


    private void Start()
    {
        if (squads.Length == 0)
        {
            readyToWork = false;
        }
        else
        {
            readyToWork = true;
            readyToNextSpawn = true;
        }
    }

    private void Update()
    {
        if (readyToWork == false) return;

        if (readyToWork && readyToNextSpawn)
        {
            // Запустить следующий спавн
            currentSquad++;

            // есть ли что запускать дальше
            if (currentSquad > squads.Length - 1)
            {
                readyToNextSpawn = false;
                readyToWork = false;
                return;
            }

            for (int i = 0; i < squads[currentSquad].Count; i++)
            {
                Enemy e = Instantiate(squads[currentSquad].EnemyPrefab);
                e.transform.position = squads[currentSquad].SpawnArea.GetRandomInsideZone();
                // задать поведение на преследование игрока

                if (squads[currentSquad].SpawnType == SpawnType.SpawnByDeath)
                {
                    e.EventOnDeath.AddListener(OnEnemyDeath);
                    // добавить отписку от этого события. Может всех заспавленных сохранять в массив и при окончании волны отписываться
                }
            }

            if (squads[currentSquad].SpawnType == SpawnType.SpawnByTime)
            {
                currentSpawnTime = squads[currentSquad].SpawnTime;
            }
            if (squads[currentSquad].SpawnType == SpawnType.SpawnByDeath)
            {
                enemyCount = squads[currentSquad].Count;
            }

            readyToNextSpawn = false;
        }

        if (readyToWork && readyToNextSpawn == false)
        {
            if (squads[currentSquad].SpawnType == SpawnType.SpawnByTime)
            {
                currentSpawnTime -= Time.deltaTime;
                if (currentSpawnTime <= 0)
                {
                    readyToNextSpawn = true;
                }
            }
        }
    }


    /// <summary>
    /// При смерти врага
    /// </summary>
    private void OnEnemyDeath()
    {
        enemyCount--;

        if (squads[currentSquad].SpawnType == SpawnType.SpawnByDeath && enemyCount == 0)
        {
            readyToNextSpawn = true;
        }
    }
}
