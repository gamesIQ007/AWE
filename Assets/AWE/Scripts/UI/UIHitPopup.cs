using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Всплывающее сообщение о нанесённом уроне
/// </summary>
public class UIHitPopup : MonoBehaviour
{
    /// <summary>
    /// Текст с нанесённым уроном
    /// </summary>
    [SerializeField] private Text damageText;


    /// <summary>
    /// Задать урон
    /// </summary>
    /// <param name="damage">Урон</param>
    public void SetDamageResult(int damage)
    {
        if (damage <= 0) return;

        damageText.text = "-" + damage.ToString("F0");
    }
}
