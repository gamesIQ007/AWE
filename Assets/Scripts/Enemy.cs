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
    /// Снаряд, которым атакует
    /// </summary>
    //[SerializeField] private Projectile projectile;
}
