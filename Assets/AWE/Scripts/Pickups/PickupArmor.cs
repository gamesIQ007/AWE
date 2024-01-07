using UnityEngine;


/// <summary>
/// Подбираемый предмет - броня
/// </summary>
public class PickupArmor : Pickup
{
    /// <summary>
    /// Количество добавляемой брони
    /// </summary>
    [SerializeField] private int addArmorPoints;

    /// <summary>
    /// Эффект при подборе
    /// </summary>
    [SerializeField] private GameObject impactEffect;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Character player = collision.GetComponent<Character>();

        if (player != null)
        {
            player.AddArmorPoints(addArmorPoints);

            if (impactEffect != null)
            {
                Instantiate(impactEffect);
            }
        }
    }
}
