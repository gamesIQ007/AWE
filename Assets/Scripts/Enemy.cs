using UnityEngine;

/// <summary>
/// Враг
/// </summary>
public class Enemy : Destructible
{
    /// <summary>
    /// Скорость перемещения
    /// </summary>
    [SerializeField] private float movementSpeed;

    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private float damage;

    /// <summary>
    /// Расстояние атаки
    /// </summary>
    [SerializeField] private float attackDistance;

    /// <summary>
    /// Расстояние обнаружения
    /// </summary>
    [SerializeField] private float detectDistance;
    public float DetectDistance => detectDistance;

    /// <summary>
    /// Снаряд, которым атакует
    /// </summary>
    //[SerializeField] private Projectile projectile;

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
    /// Перемещение к цели
    /// </summary>
    /// <param name="target">Позиция цели</param>
    public void MoveTo(GameObject go)
    {
        rb.velocity += MovementDirectionTo(go) * movementSpeed;
        //transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
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
}
