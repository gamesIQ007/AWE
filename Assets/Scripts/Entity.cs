using UnityEngine;

/// <summary>
/// Базовый класс всех интерактивных игровых объектов.
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Название объекта для пользователя.
    /// </summary>
    [SerializeField] private string nickname;
    public string Nickname => nickname;
}
