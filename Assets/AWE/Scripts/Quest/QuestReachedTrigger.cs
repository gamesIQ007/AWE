using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]

/// <summary>
/// Квест на достижение области
/// </summary>
public class QuestReachedTrigger : Quest
{
    /// <summary>
    /// Кто выполняет квест
    /// </summary>
    [SerializeField] private GameObject owner;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != owner) return;

        Completed?.Invoke();
    }
}