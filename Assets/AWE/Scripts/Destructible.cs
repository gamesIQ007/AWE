using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Уничтожаемый объект. То, что может иметь хитпоинты.
/// </summary>
public class Destructible : Entity
{
    /// <summary>
    /// Объект игнорирует повреждения.
    /// </summary>
    [SerializeField] private bool indestructible;
    public bool IsIndestructible => indestructible;

    /// <summary>
    /// Максимальное количество хитпоинтов.
    /// </summary>
    [SerializeField] protected int maxHitPoints;
    public int MaxHitPoints => maxHitPoints;

    /// <summary>
    /// Текущее количество хитпоинтов.
    /// </summary>
    protected int currentHitPoints;
    public int HitPoints => currentHitPoints;

    /// <summary>
    /// Ивент, происходящий со смертью
    /// </summary>
    [SerializeField] protected UnityEvent eventOnDeath;
    public UnityEvent EventOnDeath => eventOnDeath;

    /// <summary>
    /// Событие при изменении количества здоровья
    /// </summary>
    public UnityEvent ChangeHitPoints;


    protected virtual void Start()
    {
        currentHitPoints = maxHitPoints;
        ChangeHitPoints?.Invoke();
    }


    /// <summary>
    /// Применение урона к объекту.
    /// </summary>
    /// <param name="damage">Наносимый объекту урон.</param>
    public virtual void ApplyDamage(int damage)
    {
        if (indestructible) return;

        currentHitPoints -= damage;

        ChangeHitPoints?.Invoke();

        if (currentHitPoints <= 0)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Переопределяемое событие уничтожения объекта, когда хитпоинты меньше 0.
    /// </summary>
    protected virtual void OnDeath()
    {
        Destroy(gameObject);
        eventOnDeath?.Invoke();
    }

    /// <summary>
    /// Добавление здоровья
    /// </summary>
    /// <param name="addHitPoints">Количество добавляемого здоровья</param>
    public void AddHitPoints(int addHitPoints)
    {
        currentHitPoints += addHitPoints;
        if (currentHitPoints > maxHitPoints)
        {
            currentHitPoints = maxHitPoints;
        }
        ChangeHitPoints?.Invoke();
    }

    /// <summary>
    /// Список всех уничтожаемых объектов. HashSet - аналог List, иногда работает быстрее
    /// </summary>
    private static HashSet<Destructible> allDestructibles;
    public static IReadOnlyCollection<Destructible> AllDestructibles => allDestructibles;

    protected virtual void OnEnable()
    {
        if (allDestructibles == null)
        {
            allDestructibles = new HashSet<Destructible>();
        }

        allDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        allDestructibles.Remove(this);
    }
}
