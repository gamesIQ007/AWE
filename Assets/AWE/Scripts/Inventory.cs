using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Инвентарь
/// </summary>
public class Inventory : MonoBehaviour
{
    [System.Serializable]
    /// <summary>
    /// Информация об имеющемся оружии
    /// </summary>
    public class WeaponAndAmmo
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
    /// Ивент при изменении количества боеприпасов
    /// </summary>
    public UnityEvent<WeaponProperties, int> ChangeAmmoCount;

    /// <summary>
    /// Имеющееся оружие
    /// </summary>
    [SerializeField] private WeaponAndAmmo[] weapons;
    public WeaponAndAmmo[] WeaponsInInventory => weapons;

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    private Character player;


    private void Start()
    {
        player = GetComponent<Character>();
        RefreshAmmoCountMax();
    }


    /// <summary>
    /// Обновить максимальное количество боеприпасов
    /// </summary>
    public void RefreshAmmoCountMax()
    {
        if (player == null) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].AmmoCountMax = Mathf.RoundToInt(weapons[i].WeaponProperties.AmmoCountMaxBase * player.Characteristics.Strenght);
        }
    }

    /// <summary>
    /// Попробовать вычесть количество боеприпасов
    /// </summary>
    /// <param name="properties">Свойства оружия</param>
    /// <param name="count">Количество</param>
    /// <returns>Есть ли нужное количество боеприпасов</returns>
    public bool TryRemoveWeaponAmmo(WeaponProperties properties, int count)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].WeaponProperties != properties) continue;

            if (weapons[i].AmmoCountCurrent < count) return false;

            weapons[i].AmmoCountCurrent -= count;

            ChangeAmmoCount?.Invoke(properties, weapons[i].AmmoCountCurrent);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Добавить боеприпасы
    /// </summary>
    /// <param name="properties">Свойства оружия</param>
    /// <param name="count">Количество</param>
    public void AddWeaponAmmo(WeaponProperties properties, int count)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].WeaponProperties == properties)
            {
                weapons[i].AmmoCountCurrent += count;

                if (weapons[i].AmmoCountCurrent > weapons[i].AmmoCountMax)
                {
                    weapons[i].AmmoCountCurrent = weapons[i].AmmoCountMax;
                }

                ChangeAmmoCount?.Invoke(properties, weapons[i].AmmoCountCurrent);
            }
        }
    }
}
