using UnityEngine;


/// <summary>
/// Отображение оружия
/// </summary>
public class WeaponVisual : MonoBehaviour
{
    /// <summary>
    /// Цель оружия
    /// </summary>
    public enum WeaponTarget
    {
        /// <summary>
        /// Курсор
        /// </summary>
        Cursor,
        /// <summary>
        /// Объект
        /// </summary>
        Object
    }


    /// <summary>
    /// Цель оружия
    /// </summary>
    [SerializeField] private WeaponTarget weaponTarget;

    /// <summary>
    /// Скорость поворота
    /// </summary>
    [SerializeField] private int speed;

    /// <summary>
    /// Цель
    /// </summary>
    [SerializeField] private GameObject target;


    private void Update()
    {
        if (weaponTarget == WeaponTarget.Cursor)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
        if (weaponTarget == WeaponTarget.Object)
        {
            if (target == null) return;

            Vector3 difference = target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, rotationZ), speed * Time.deltaTime);
        }
    }


    /// <summary>
    /// Задать цель
    /// </summary>
    /// <param name="target">Цель</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
