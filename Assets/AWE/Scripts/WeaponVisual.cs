using UnityEngine;


/// <summary>
/// Отображение оружия
/// </summary>
public class WeaponVisual : MonoBehaviour
{
    /// <summary>
    /// Спрайт
    /// </summary>
    [SerializeField] private SpriteRenderer sr;

    /// <summary>
    /// Ригид
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Смотрим ли вправо
    /// </summary>
    private bool lookRight;


    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
