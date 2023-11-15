using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображение здоровья в интерфейсе
/// </summary>
public class UIHPBar : MonoBehaviour
{
    /// <summary>
    /// Игрок
    /// </summary>
    [SerializeField] private Character player;

    /// <summary>
    /// Текст, отображающий количество здоровья
    /// </summary>
    [SerializeField] private Text hpText;


    private void Start()
    {
        player.ChangeHitPoints.AddListener(OnChangeHitPoints);
        OnChangeHitPoints();
    }

    private void OnDestroy()
    {
        player.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }


    /// <summary>
    /// При изменении очков здоровья
    /// </summary>
    private void OnChangeHitPoints()
    {
        hpText.text = player.HitPoints.ToString();
    }
}
