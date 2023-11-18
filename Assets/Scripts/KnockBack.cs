using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// Отшвыривание персонажа
/// </summary>
public class KnockBack : MonoBehaviour
{
    /// <summary>
    /// Сила отшвыривания
    /// </summary>
    [SerializeField] private float force;

    /// <summary>
    /// Задержка между применениями
    /// </summary>
    [SerializeField] private float delay = 0.1f;

    /// <summary>
    /// Таймер
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// Ссылка на ригид
    /// </summary>
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Применить отбрасывание в противоположную сторону
    /// </summary>
    /// <param name="other">Кто отбрасывается</param>
    public void ApplyKnockBack(Transform other)
    {
        if (timer > 0)
        {
            return;
        }

        Vector2 direction = -(other.position - transform.position).normalized;

        rb.AddForce(direction * (force * 100f), ForceMode2D.Impulse);

        timer = delay;
    }
}
