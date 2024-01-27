using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Здоровье босса
/// </summary>
public class UIBossHealth : MonoBehaviour
{
    /// <summary>
    /// Босс
    /// </summary>
    [SerializeField] private Boss boss;

    /// <summary>
    /// Слайдер, показывающий здоровье босса
    /// </summary>
    [SerializeField] private Slider slider;


    private void Start()
    {
        boss.ChangeHitPoints.AddListener(OnChangeHitPoints);
        slider.maxValue = boss.MaxHitPoints;
        slider.value = boss.HitPoints;
    }


    private void OnChangeHitPoints(int damage, Vector2 position)
    {
        slider.value = boss.HitPoints;
    }
}
