using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/// <summary>
/// Дверь
/// </summary>
public class Door : MonoBehaviour
{
    [Header("Models")]
    /// <summary>
    /// Открытая дверь
    /// </summary>
    [SerializeField] private GameObject openDoor;
    /// <summary>
    /// Закрытая дверь
    /// </summary>
    [SerializeField] private GameObject closedDoor;

    [Header("Indicators")]
    /// <summary>
    /// Индикатор открытой двери
    /// </summary>
    [SerializeField] private GameObject openDoorIndicator;
    /// <summary>
    /// Индикатор закрытой двери
    /// </summary>
    [SerializeField] private GameObject closedDoorIndicator;

    /// <summary>
    /// Эффект при открытии двери
    /// </summary>
    [SerializeField] private ImpactEffect openingEffect;
    /// <summary>
    /// Эффект при закрытии двери
    /// </summary>
    [SerializeField] private ImpactEffect closingEffect;

    [Header("Settings")]
    /// <summary>
    /// Статус двери
    /// </summary>
    [SerializeField] private bool doorIsOpen = true;

    
    private void Start()
    {
        openDoor.SetActive(false);
        openDoorIndicator.SetActive(doorIsOpen);
        closedDoorIndicator.SetActive(!doorIsOpen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorIsOpen == false) return;

        if (collision.transform.root.GetComponent<Character>() != null)
        {
            closedDoor.SetActive(false);
            openDoor.SetActive(true);
            if (openingEffect != null)
            {
                Instantiate(openingEffect, transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (doorIsOpen == false) return;

        if (collision.transform.root.GetComponent<Character>() != null)
        {
            openDoor.SetActive(false);
            closedDoor.SetActive(true);
            if (closingEffect != null)
            {
                Instantiate(closingEffect, transform);
            }
        }
    }


    /// <summary>
    /// Открыть дверь
    /// </summary>
    public void OpenDoor()
    {
        doorIsOpen = true;
        openDoorIndicator.SetActive(doorIsOpen);
        closedDoorIndicator.SetActive(!doorIsOpen);
    }
}
