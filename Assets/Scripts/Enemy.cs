using UnityEngine;


/// <summary>
/// Тип врага
/// </summary>
public enum EnemyType
{
    /// <summary>
    /// Ближний бой
    /// </summary>
    Melee,
    /// <summary>
    /// Дальний бой
    /// </summary>
    Shooter
}


/// <summary>
/// Враг
/// </summary>
public class Enemy : Destructible
{
    /// <summary>
    /// Тип врага
    /// </summary>
    [SerializeField] private EnemyType type;
    public EnemyType Type => type;

    /// <summary>
    /// Скорость перемещения
    /// </summary>
    [SerializeField] private float movementSpeed;

    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private float damage;

    /// <summary>
    /// Расстояние атаки ближнего боя
    /// </summary>
    [SerializeField] private float meleeAttackDistance;
    public float MeleeAttackDistance => meleeAttackDistance;

    /// <summary>
    /// Расстояние атаки дальнего боя
    /// </summary>
    [SerializeField] private float shootAttackDistance;
    public float ShootAttackDistance => shootAttackDistance;

    /// <summary>
    /// Расстояние обнаружения
    /// </summary>
    [SerializeField] private float detectDistance;
    public float DetectDistance => detectDistance;

    /// <summary>
    /// Атака ближнего боя
    /// </summary>
    [SerializeField] private MeleeAttack meleeAttack;
    public MeleeAttack MeleeAttack => meleeAttack;

    /// <summary>
    /// Снаряд, которым атакует
    /// </summary>
    [SerializeField] private Projectile projectile;

    /// <summary>
    /// Жив ли?
    /// </summary>
    private bool isDead;
    public bool IsDead => isDead;

    /// <summary>
    /// Направление движения
    /// </summary>
    private Vector2 movementDirection = Vector2.zero;

    /// <summary>
    /// Ссылка на ригид
    /// </summary>
    private Rigidbody2D rb;


    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// Направление движения
    /// </summary>
    /// <param name="go">Объект, в сторону которого нужно двигаться</param>
    /// <returns>Направление движения</returns>
    private Vector2 MovementDirectionTo(GameObject go)
    {
        return (go.transform.position - transform.position).normalized;
    }


    /// <summary>
    /// Перемещение к цели
    /// </summary>
    /// <param name="target">Позиция цели</param>
    public void MoveTo(GameObject go)
    {
        rb.velocity += MovementDirectionTo(go) * movementSpeed;
    }

    /// <summary>
    /// Атаковать ближним боем
    /// </summary>
    /// <param name="attackPosition">Точка, в которую атакует</param>
    public void AttackMeleeWeapon(Vector3 attackPosition)
    {
        if (meleeAttack != null)
        {
            meleeAttack.Attack(attackPosition);
        }
    }

    public void AttackDistanceWeapon(Vector3 attackPosition)
    {
        Vector3 difference = attackPosition - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, rotationZ - 90.0f);

        transform.localRotation = Quaternion.identity;
        GameObject obj = Instantiate(projectile.gameObject);
        obj.transform.position = transform.position;
        obj.transform.rotation = rot;
    }
}
