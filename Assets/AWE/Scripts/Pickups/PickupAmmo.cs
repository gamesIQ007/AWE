using UnityEngine;


/// <summary>
/// Подбираемый предмет - боеприпасы
/// </summary>
public class PickupAmmo : Pickup
{
    [System.Serializable]
    /// <summary>
    /// Пак боеприпасов
    /// </summary>
    private class AmmoPack
    {
        /// <summary>
        /// Свойства оружия
        /// </summary>
        public WeaponProperties Properties;
        /// <summary>
        /// Количество боеприпасов
        /// </summary>
        public int Count;
    }


    /// <summary>
    /// Паки боеприпасов
    /// </summary>
    [SerializeField] private AmmoPack[] ammoPacks;

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
            for (int i = 0; i < ammoPacks.Length; i++)
            {
                player.Inventory.AddWeaponAmmo(ammoPacks[i].Properties, ammoPacks[i].Count);
            }

            if (impactEffect != null)
            {
                Instantiate(impactEffect);
            }
        }
    }
}
