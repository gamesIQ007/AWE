using UnityEngine;


/// <summary>
/// Босс
/// </summary>
public class Boss : Enemy
{
    /// <summary>
    /// Оружие
    /// </summary>
    [SerializeField] private Weapon[] weapons;
    public Weapon[] Weapons => weapons;

    /// <summary>
    /// Щит
    /// </summary>
    [SerializeField] private GameObject shield;
    public GameObject Shield => shield;


    /// <summary>
    /// Атаковать дальнобойным оружием
    /// </summary>
    /// <param name="attackPosition">Позиция, в которую производится выстрел</param>
    public override void AttackDistanceWeapon(Vector3 attackPosition)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Fire();
        }
    }
}
