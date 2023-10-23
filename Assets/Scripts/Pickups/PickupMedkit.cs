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

        Player player = collision.GetComponent<Player>();

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
