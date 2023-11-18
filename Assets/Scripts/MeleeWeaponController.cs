using UnityEngine;


/// <summary>
/// Контроллер поведения оружия ближнего боя
/// </summary>
public class MeleeWeaponController : MonoBehaviour
{
    /// <summary>
    /// Оружие ближнего боя игрока
    /// </summary>
    private MeleeAttack playerMeleeWeapon;

    /// <summary>
    /// Начальное вращение
    /// </summary>
    private Quaternion startRotation;
    /// <summary>
    /// Конечное вращение
    /// </summary>
    private Quaternion endRotation;

    /// <summary>
    /// Таймер
    /// </summary>
    private float timer;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > playerMeleeWeapon.Speed)
        {
            Destroy(gameObject);
        }

        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, timer / playerMeleeWeapon.Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible dest = collision.GetComponent<Destructible>();

        Character player = collision.GetComponent<Character>();

        if (player == null && dest != null)
        {
            dest.ApplyDamage(playerMeleeWeapon.Damage);
            dest.GetComponent<KnockBack>().ApplyKnockBack(transform);
        }
    }


    /// <summary>
    /// Инициализация атаки
    /// </summary>
    /// <param name="playerMeleeWeapon">Оружие ближнего боя игрока</param>
    public void InitializeAttack(MeleeAttack playerMeleeWeapon)
    {
        this.playerMeleeWeapon = playerMeleeWeapon;

        ComputeSwingRotations();
        
        transform.localRotation = startRotation;
        
        timer = 0;
    }


    /// <summary>
    /// Рассчитать начальное и конечное вращение
    /// </summary>
    private void ComputeSwingRotations()
    {
        Quaternion rotation = transform.rotation;
        startRotation = rotation * Quaternion.Euler(0, 0, -playerMeleeWeapon.SwingAngle);
        endRotation = rotation * Quaternion.Euler(0, 0, playerMeleeWeapon.SwingAngle);
    }
}
