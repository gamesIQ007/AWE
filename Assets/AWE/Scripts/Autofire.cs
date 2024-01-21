using UnityEngine;


/// <summary>
/// Автоматическая стрельба из оружия. Вешается на оружие
/// </summary>
public class Autofire : MonoBehaviour
{
    /// <summary>
    /// Оружие
    /// </summary>
    [SerializeField] private Weapon weapon;


    private void Update()
    {
        weapon.Fire();
    }
}
