using UnityEngine;


[RequireComponent(typeof(Character))]

/// <summary>
/// Оружие ближнего боя игрока
/// </summary>
public class MeleeAttack : MonoBehaviour
{
    /// <summary>
    /// Оружие ближнего боя
    /// </summary>
    [SerializeField] private GameObject weapon;

    [Header("Attack Parameters")]

    // ПЕРЕДЕЛАТЬ ЧТОБЫ БРАЛОСЬ ИЗ ПАРАМЕТРОВ
    /// <summary>
    /// Время между атаками
    /// </summary>
    [SerializeField] private float delay;
    public float Delay => delay;

    // ПЕРЕДЕЛАТЬ ЧТОБЫ БРАЛОСЬ ИЗ ПАРАМЕТРОВ
    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private int damage;
    public int Damage => damage;

    /// <summary>
    /// Скорость движения оружия
    /// </summary>
    [SerializeField] private float speed;
    public float Speed => speed;

    /// <summary>
    /// Угол перемещения оружия
    /// </summary>
    [SerializeField] private float swingAngle;
    public float SwingAngle => swingAngle;

    /// <summary>
    /// Сохранённая ссылка на игрока
    /// </summary>
    private Character player;

    /// <summary>
    /// Таймер
    /// </summary>
    private float timer = 0;


    private void Start()
    {
        player = GetComponent<Character>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Атака
    /// </summary>
    public void Attack()
    {
        if (timer <= 0)
        {
            InstantiateAttack();
            timer = delay;
        }
    }

    /// <summary>
    /// Создать атаку ближнего боя
    /// </summary>
    private void InstantiateAttack()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        transform.localRotation = Quaternion.identity;
        GameObject obj = Instantiate(weapon, transform.position, Quaternion.Euler(0, 0, rotationZ), transform);
        MeleeWeaponController attackController = obj.GetComponent<MeleeWeaponController>();
        attackController.InitializeAttack(this);
    }
}
