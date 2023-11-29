using System.Collections;
using UnityEngine;


/// <summary>
/// Панель информации о количестве патронов оружия
/// </summary>
public class UIWeaponAmmoPanel : MonoBehaviour
{
    /// <summary>
    /// Связь блока с информацией о патронах оружия и свойствами оружия
    /// </summary>
    private class WeaponBlockProperties
    {
        /// <summary>
        /// Свойства оружия
        /// </summary>
        public WeaponProperties Properties;
        /// <summary>
        /// Блок с информацией о количестве патронов оружия
        /// </summary>
        public UIWeaponAmmoBlock WeaponAmmoBlock;
    }


    /// <summary>
    /// Блок с информацией о количестве патронов оружия
    /// </summary>
    [SerializeField] private UIWeaponAmmoBlock weaponAmmoBlock;

    /// <summary>
    /// Панель с блоками с количеством оружия
    /// </summary>
    [SerializeField] private UIWeaponAmmoPanel weaponAmmoPanel;

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    [SerializeField] private Character player;

    /// <summary>
    /// Массив связей свойств оружия и блоков, отображающих боеприпасы этого оружия
    /// </summary>
    private WeaponBlockProperties[] blockProperties;


    private void Start()
    {
        player.Inventory.ChangeAmmoCount.AddListener(OnChangeAmmoCount);

        blockProperties = new WeaponBlockProperties[player.Inventory.WeaponsInInventory.Length];
        
        for (int i = 0; i < blockProperties.Length; i++)
        {
            blockProperties[i] = new WeaponBlockProperties();

            blockProperties[i].Properties = player.Inventory.WeaponsInInventory[i].WeaponProperties;
            blockProperties[i].WeaponAmmoBlock = Instantiate(weaponAmmoBlock);
            blockProperties[i].WeaponAmmoBlock.transform.SetParent(weaponAmmoPanel.transform);
            blockProperties[i].WeaponAmmoBlock.SetAmmoCount(player.Inventory.WeaponsInInventory[i].AmmoCountCurrent);
            blockProperties[i].WeaponAmmoBlock.enabled = player.Inventory.WeaponsInInventory[i].IsAvailable;
        }

        //StartCoroutine(InitiateAmmoPanel());
    }

    private void OnDestroy()
    {
        player.Inventory.ChangeAmmoCount.RemoveListener(OnChangeAmmoCount);
    }


    IEnumerator InitiateAmmoPanel()
    {
        yield return new WaitForSeconds(0.5f);

        blockProperties = new WeaponBlockProperties[player.Inventory.WeaponsInInventory.Length];

        for (int i = 0; i < blockProperties.Length; i++)
        {
            blockProperties[i].Properties = player.Inventory.WeaponsInInventory[i].WeaponProperties;
            blockProperties[i].WeaponAmmoBlock = Instantiate(weaponAmmoBlock);
            blockProperties[i].WeaponAmmoBlock.transform.SetParent(weaponAmmoPanel.transform);
            blockProperties[i].WeaponAmmoBlock.SetAmmoCount(player.Inventory.WeaponsInInventory[i].AmmoCountCurrent);
            blockProperties[i].WeaponAmmoBlock.enabled = player.Inventory.WeaponsInInventory[i].IsAvailable;
        }
    }

    /// <summary>
    /// Действие при изменении количества боеприпасов
    /// </summary>
    /// <param name="properties">Свойства оружия</param>
    /// <param name="ammoCount">Количество боеприпасов</param>
    private void OnChangeAmmoCount(WeaponProperties properties, int ammoCount)
    {
        for (int i = 0; i < blockProperties.Length; i++)
        {
            if (blockProperties[i].Properties == properties)
            {
                blockProperties[i].WeaponAmmoBlock.SetAmmoCount(ammoCount);
            }
        }
    }
}
