using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Триггер, срабатывающий попадание в него игрока
/// </summary>
public class Trigger : MonoBehaviour
{
    /// <summary>
    /// Событие при достижении точки
    /// </summary>
    [SerializeField] private UnityEvent activateTrigger;
    public UnityEvent ActivateTrigger => activateTrigger;

    /// <summary>
    /// Одноразовый ли триггер?
    /// </summary>
    [SerializeField] private bool oneUseTrigger = false;

    /// <summary>
    /// Эффект при спавне
    /// </summary>
    [SerializeField] private ImpactEffect impactEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = collision.GetComponent<Character>();

        if (player != null)
        {
            activateTrigger.Invoke();

            // Спавним эффект
            if (impactEffect != null)
            {
                Instantiate(impactEffect);
            }

            if (oneUseTrigger)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}