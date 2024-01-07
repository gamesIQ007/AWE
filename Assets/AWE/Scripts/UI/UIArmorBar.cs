using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Отображение брони в интерфейсе
/// </summary>
public class UIArmorBar : MonoBehaviour
{
    /// <summary>
    /// Игрок
    /// </summary>
    [SerializeField] private Character player;

    /// <summary>
    /// Текст, отображающий количество брони
    /// </summary>
    [SerializeField] private Text armorText;


    private void Start()
    {
        player.ChangeArmorPoints.AddListener(OnChangeArmorPoints);
        OnChangeArmorPoints();
    }

    private void OnDestroy()
    {
        player.ChangeArmorPoints.RemoveListener(OnChangeArmorPoints);
    }


    /// <summary>
    /// При изменении очков брони
    /// </summary>
    private void OnChangeArmorPoints()
    {
        armorText.text = player.Armor.ToString();
    }
}
