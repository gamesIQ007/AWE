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

    /// <summary>
    /// Здоровье
    /// </summary>
    private int hp;


    private void Start()
    {
        player.ChangeHitPoints.AddListener(OnChangeHitPoints);
        OnChangeHitPoints(player.HitPoints, player.transform.position);
    }

    private void OnDestroy()
    {
        player.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }


    /// <summary>
    /// При изменении очков здоровья
    /// </summary>
    private void OnChangeHitPoints(int damage, Vector2 position)
    {
        hp = player.HitPoints;

        if (hp < 0)
        {
            hp = 0;
        }

        hpText.text = hp.ToString();
    }
}
