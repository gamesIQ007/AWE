using System;
using UnityEngine;


/// <summary>
/// Спавнер
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Типы запуска следующего спавна
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
    /// Класс пачки спавна
    /// </summary>
    [Serializable]
    private class SpawnPack
    {
        /// <summary>
        /// Префаб для спавна
        /// </summary>
        public GameObject SpawnPrefab;
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
    /// Пачки спавна
    /// </summary>
    [SerializeField] private SpawnPack[] spawnPacks;

    /// <summary>
    /// Эффект при спавне
    /// </summary>
    [SerializeField] private ImpactEffect impactEffect;

    /// <summary>
    /// Индекс текущей пачки спавна
    /// </summary>
    private int currentPack = -1;

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
        if (spawnPacks.Length == 0)
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
            currentPack++;

            // есть ли что запускать дальше
            if (currentPack > spawnPacks.Length - 1)
            {
                readyToNextSpawn = false;
                readyToWork = false;
                return;
            }

            for (int i = 0; i < spawnPacks[currentPack].Count; i++)
            {
                GameObject go = Instantiate(spawnPacks[currentPack].SpawnPrefab);
                go.transform.position = spawnPacks[currentPack].SpawnArea.GetRandomInsideZone();
                // задать поведение на преследование игрока

                if (spawnPacks[currentPack].SpawnType == SpawnType.SpawnByDeath)
                {
                    if (go.GetComponent<Enemy>())
                    {
                        go.GetComponent<Enemy>().EventOnDeath.AddListener(OnEnemyDeath);
                        // добавить отписку от этого события. Может всех заспавленных сохранять в массив и при окончании волны отписываться
                    }
                }

                // Спавним эффект
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, go.transform);
                }
            }

            if (spawnPacks[currentPack].SpawnType == SpawnType.SpawnByTime)
            {
                currentSpawnTime = spawnPacks[currentPack].SpawnTime;
            }
            if (spawnPacks[currentPack].SpawnType == SpawnType.SpawnByDeath)
            {
                enemyCount = spawnPacks[currentPack].Count;
            }

            readyToNextSpawn = false;
        }

        if (readyToWork && readyToNextSpawn == false)
        {
            if (spawnPacks[currentPack].SpawnType == SpawnType.SpawnByTime)
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

        if (spawnPacks[currentPack].SpawnType == SpawnType.SpawnByDeath && enemyCount == 0)
        {
            readyToNextSpawn = true;
        }
    }
}
