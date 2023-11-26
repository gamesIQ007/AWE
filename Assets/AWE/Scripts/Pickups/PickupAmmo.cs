using UnityEngine;


/// <summary>
/// ����������� ������� - ����������
/// </summary>
public class PickupAmmo : Pickup
{
    [System.Serializable]
    /// <summary>
    /// ��� �����������
    /// </summary>
    private class AmmoPack
    {
        /// <summary>
        /// �������� ������
        /// </summary>
        public WeaponProperties Properties;
        /// <summary>
        /// ���������� �����������
        /// </summary>
        public int Count;
    }


    /// <summary>
    /// ���� �����������
    /// </summary>
    [SerializeField] private AmmoPack[] ammoPacks;

    /// <summary>
    /// ������ ��� �������
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
