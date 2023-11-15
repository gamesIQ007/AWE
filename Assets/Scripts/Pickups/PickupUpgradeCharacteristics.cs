using UnityEngine;

/// <summary>
/// Подбираемый предмет - апгрейд характеристик
/// </summary>
public class PickupUpgradeCharacteristics : Pickup
{
    /// <summary>
    /// Характеристика
    /// </summary>
    [SerializeField] private Characteristics characteristic;

    /// <summary>
    /// Количество добавляемых очков
    /// </summary>
    [SerializeField] private int countPoints = 1;

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
            player.Characteristics.AddCharacteristic(characteristic, countPoints);

            if (impactEffect != null)
            {
                Instantiate(impactEffect);
            }
        }
    }
}
