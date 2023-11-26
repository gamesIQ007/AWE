using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Инвентарь
/// </summary>
public class Inventory : MonoBehaviour
{
    [System.Serializable]
    /// <summary>
    /// Информация об имеющемся оружии
    /// </summary>
    public class Weapons
    {
        /// <summary>
        /// Свойства оружия
        /// </summary>
        public WeaponProperties WeaponProperties;

        /// <summary>
        /// Имеется ли в наличии
        /// </summary>
        public bool IsAvailable;

        /// <summary>
        /// Максимальное количество боеприпасов
        /// </summary>
        public int AmmoCountMax;

        /// <summary>
        /// Текущее количество боеприпасов
        /// </summary>
        public int AmmoCountCurrent;
    }


    /// <summary>
    /// Имеющееся оружие
    /// </summary>
    [SerializeField] private Weapons[] weapons;
    public Weapons[] WeaponsInInventory => weapons;

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    private Character player;


    private void Start()
    {
        player = GetComponent<Character>();
    }


    /// <summary>
    /// Обновить максимальное количество боеприпасов
    /// </summary>
    private void RefreshAmmoCountMax()
    {
        if (player == null) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].AmmoCountMax = (int)(weapons[i].WeaponProperties.AmmoCountMaxBase * player.Characteristics.Strenght);
        }
    }
}
