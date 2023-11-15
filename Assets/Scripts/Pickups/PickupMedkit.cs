using UnityEngine;

/// <summary>
/// Подбираемый предмет - аптечка
/// </summary>
public class PickupMedkit : Pickup
{
    /// <summary>
    /// Количество добавляемого здоровья
    /// </summary>
    [SerializeField] private int addHitPoints;

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
            player.AddHitPoints(addHitPoints);

            if (impactEffect != null)
            {
                Instantiate(impactEffect);
            }
        }
    }
}
