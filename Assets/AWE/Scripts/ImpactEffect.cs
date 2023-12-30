using UnityEngine;


[RequireComponent(typeof(AudioSource))]

/// <summary>
/// Эффект
/// </summary>
public class ImpactEffect : MonoBehaviour
{
    /// <summary>
    /// Время жизни
    /// </summary>
    [SerializeField] private float lifeTime;


    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
