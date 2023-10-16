using UnityEngine;


/// <summary>
/// Отображение персонажа
/// </summary>
public class CharacterVisual : MonoBehaviour
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
    /// Анимации
    /// </summary>
    //private Animator animator;

    /// <summary>
    /// Смотрим ли вправо
    /// </summary>
    private bool lookRight;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    /*private void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }
    }*/

    private void LateUpdate()
    {
        transform.up = Vector2.up;

        if (lookRight && rb.velocity.x < 0)
        {
            sr.flipX = true;
            lookRight = false;
        }
        else if (lookRight == false && rb.velocity.x > 0)
        {
            sr.flipX = false;
            lookRight = true;
        }
    }
}
