using UnityEngine;


/// <summary>
/// Перечень характеристик
/// </summary>
public enum Characteristics
{
    HP,
    Strenght,
    Speed,
    Stamina,
    Accuracy,
    Agility
}


/// <summary>
/// Характеристики игрока
/// </summary>
public class PlayerCharacteristics : MonoBehaviour
{
    [Header("Base Characteristics")]
    /// <summary>
    /// Базовое количество здоровья
    /// </summary>
    [SerializeField] private int baseHp;
    /// <summary>
    /// Базовая сила
    /// </summary>
    [SerializeField] private int baseStrenght;
    /// <summary>
    /// Базовая скорость
    /// </summary>
    [SerializeField] private float baseSpeed;
    /// <summary>
    /// Базовая выносливость
    /// </summary>
    [SerializeField] private int baseStamina;
    /// <summary>
    /// Базовая точность
    /// </summary>
    [SerializeField] private int baseAccuracy;
    /// <summary>
    /// Базовая ловкость
    /// </summary>
    [SerializeField] private int baseAgility;

    [Header("Characteristics Points")]
    /// <summary>
    /// Здоровье - количество очков здоровья
    /// </summary>
    [SerializeField] private int hpPoints;
    /// <summary>
    /// Сила - количество переносимых патронов
    /// </summary>
    [SerializeField] private int strenghtPoints;
    /// <summary>
    /// Скорость - скорость перемещения
    /// </summary>
    [SerializeField] private int speedPoints;
    /// <summary>
    /// Выносливость - количество выносливости и скорость её восстановления
    /// </summary>
    [SerializeField] private int staminaPoints;
    /// <summary>
    /// Точность - снижение разброса при стрельбе
    /// </summary>
    [SerializeField] private int accuracyPoints;
    /// <summary>
    /// Ловкость - вероятность увернуться от атаки или снизить урон
    /// </summary>
    [SerializeField] private int agilityPoints;
    
    [Header("Modifiers")]
    /// <summary>
    /// Модификатор здоровья
    /// </summary>
    [SerializeField] private float hpModifier;
    /// <summary>
    /// Модификатор силы
    /// </summary>
    [SerializeField] private float strenghtModifier;
    /// <summary>
    /// Модификатор скорости
    /// </summary>
    [SerializeField] private float speedModifier;
    /// <summary>
    /// Модификатор выносливости
    /// </summary>
    [SerializeField] private float staminaModifier;
    /// <summary>
    /// Модификатор точности
    /// </summary>
    [SerializeField] private float accuracyModifier;
    /// <summary>
    /// Модификатор ловкости
    /// </summary>
    [SerializeField] private float agilityModifier;


    // Доступ к характеристикам извне
    public int Hp => baseHp + (int)(hpPoints * hpModifier);
    public int Strenght => baseStrenght + (int)(strenghtPoints * strenghtModifier);
    public float Speed => baseSpeed + speedPoints * speedModifier;
    public int Stamina => baseStamina + (int)(staminaPoints * staminaModifier);
    public int Accuracy => baseAccuracy + (int)(accuracyPoints * accuracyModifier);
    public int Agility => baseAgility + (int)(agilityPoints * agilityModifier);


    // Доступ к очкам способностей извне
    public int HpPoints => hpPoints;
    public int StrenghtPoints => strenghtPoints;
    public int SpeedPoints => speedPoints;
    public int StaminaPoints => staminaPoints;
    public int AccuracyPoints => accuracyPoints;
    public int AgilityPoints => agilityPoints;


    /// <summary>
    /// Добавить очки характеристик
    /// </summary>
    /// <param name="characteristics">Характеристика</param>
    /// <param name="value">Количество добавляемых очков</param>
    public void AddCharacteristic(Characteristics characteristics, int value)
    {
        if (value <= 0) return;

        if (characteristics == Characteristics.HP)
        {
            hpPoints += value;
        }
        if (characteristics == Characteristics.Strenght)
        {
            strenghtPoints += value;
        }
        if (characteristics == Characteristics.Speed)
        {
            speedPoints += value;
        }
        if (characteristics == Characteristics.Stamina)
        {
            staminaPoints += value;
        }
        if (characteristics == Characteristics.Accuracy)
        {
            accuracyPoints += value;
        }
        if (characteristics == Characteristics.Agility)
        {
            agilityPoints += value;
        }
    }
}
