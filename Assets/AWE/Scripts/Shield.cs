using UnityEngine;


/// <summary>
/// Активируемый щит
/// </summary>
public class Shield : MonoBehaviour
{
    /// <summary>
    /// Изображение
    /// </summary>
    [SerializeField] private SpriteRenderer sr;

    /// <summary>
    /// Владелец щита
    /// </summary>
    private Destructible owner;


    private void Awake()
    {
        owner = transform.root.GetComponent<Destructible>();
        sr.enabled = true;
        owner.IndestructibleOn();
    }

    private void OnDisable()
    {
        sr.enabled = false;
        owner.IndestructibleOff();
    }

    private void OnEnable()
    {
        sr.enabled = true;
        owner.IndestructibleOn();
    }
}
