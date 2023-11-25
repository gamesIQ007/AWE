using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/// <summary>
/// Подбираемый предмет
/// </summary>
public class Pickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = collision.GetComponent<Character>();

        if (player != null)
        {
            Destroy(gameObject);
        }
    }
}
