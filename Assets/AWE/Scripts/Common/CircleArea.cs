using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// Область
/// </summary>
public class CircleArea : MonoBehaviour
{
    /// <summary>
    /// Радиус
    /// </summary>
    [SerializeField] private float radius;
    public float Radius => radius;


    /// <summary>
    /// Получить случайную точку внутри области
    /// </summary>
    /// <returns>Случайная точка внутри области</returns>
    public Vector2 GetRandomInsideZone()
    {
        return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * radius;
    }


#if UNITY_EDITOR
    /// <summary>
    /// Цвет гизмо
    /// </summary>
    private static Color gizmoColor = new Color(0, 1, 0, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = gizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, radius);
    }
#endif

}