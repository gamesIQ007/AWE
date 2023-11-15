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
    /// Перемещение к цели
    /// </summary>
    /// <param name="target">Позиция цели</param>
    public void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }
}
