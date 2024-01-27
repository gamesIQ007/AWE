using UnityEngine;


/// <summary>
/// Контроллер поведения оружия ближнего боя
/// </summary>
public class MeleeWeaponController : MonoBehaviour
{
    /// <summary>
    /// Оружие ближнего боя
    /// </summary>
    private MeleeAttack meleeWeapon;

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

        if (timer > meleeWeapon.Speed)
        {
            Destroy(gameObject);
        }

        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, timer / meleeWeapon.Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible dest = collision.GetComponent<Destructible>();

        if (dest != null && dest != meleeWeapon.Attacker)
        {
            dest.ApplyDamage(meleeWeapon.Damage);
            dest.GetComponent<KnockBack>()?.ApplyKnockBack(transform);
        }
    }


    /// <summary>
    /// Инициализация атаки
    /// </summary>
    /// <param name="meleeWeapon">Оружие ближнего боя игрока</param>
    public void InitializeAttack(MeleeAttack meleeWeapon)
    {
        this.meleeWeapon = meleeWeapon;

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
        startRotation = rotation * Quaternion.Euler(0, 0, -meleeWeapon.SwingAngle);
        endRotation = rotation * Quaternion.Euler(0, 0, meleeWeapon.SwingAngle);
    }
}
