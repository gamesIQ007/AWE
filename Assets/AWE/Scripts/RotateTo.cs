using UnityEngine;


/// <summary>
/// Поворот к цели
/// </summary>
public class RotateTo : MonoBehaviour
{
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
        if (target == null) return;

        Vector3 difference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, rotationZ), speed * Time.deltaTime);
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
