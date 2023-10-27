using UnityEngine;


/// <summary>
/// Класс для слежения камеры за целью
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Камера
    /// </summary>
    [SerializeField] private new Camera camera;

    /// <summary>
    /// Цель слежения
    /// </summary>
    [SerializeField] private Transform target;

    /// <summary>
    /// Линейная скорость
    /// </summary>
    [SerializeField] private float interpolationLinear;

    /// <summary>
    /// Скорость поворота
    /// </summary>
    [SerializeField] private float interpolationAngular;

    /// <summary>
    /// Отступ камеры по оси Z
    /// </summary>
    [SerializeField] private float cameraZOffset;

    /// <summary>
    /// Смещение камеры вверх от цели
    /// </summary>
    [SerializeField] private float forwardOffset;

    
    private void FixedUpdate()
    {
        if (camera == null || target == null) return;

        Vector2 camPos = camera.transform.position;
        Vector2 targetPos = target.position + target.transform.up * forwardOffset;

        Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, interpolationLinear * Time.deltaTime);

        camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, cameraZOffset);

        /*if (interpolationAngular > 0)
        {
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, target.rotation, interpolationAngular * Time.deltaTime);
        }*/
    }

    
    /// <summary>
    /// Назначение новой цели отслеживания камерой
    /// </summary>
    /// <param name="newTarget">Новая цель отслеживания</param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
