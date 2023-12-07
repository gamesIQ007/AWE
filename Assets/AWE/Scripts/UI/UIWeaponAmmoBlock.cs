using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Блок с информацией о количестве боеприпасов оружия
/// </summary>
public class UIWeaponAmmoBlock : MonoBehaviour
{
    /// <summary>
    /// Иконка оружия
    /// </summary>
    [SerializeField] private Image icon;

    /// <summary>
    /// Количество боеприпасов
    /// </summary>
    [SerializeField] private Text ammoCount;


    /// <summary>
    /// Задать иконку
    /// </summary>
    /// <param name="sprite">Иконка</param>
    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    /// <summary>
    /// Задать количество боеприпасов
    /// </summary>
    /// <param name="count">Количество боеприпасов</param>
    public void SetAmmoCount(int count)
    {
        ammoCount.text = count.ToString("F0");
    }
}
