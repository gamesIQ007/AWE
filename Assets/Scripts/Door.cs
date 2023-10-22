using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/// <summary>
/// �����
/// </summary>
public class Door : MonoBehaviour
{
    /// <summary>
    /// �������� �����
    /// </summary>
    [SerializeField] private GameObject openDoor;
    /// <summary>
    /// �������� �����
    /// </summary>
    [SerializeField] private GameObject closedDoor;

    
    private void Start()
    {
        openDoor.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Player>() != null)
        {
            closedDoor.SetActive(false);
            openDoor.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Player>() != null)
        {
            openDoor.SetActive(false);
            closedDoor.SetActive(true);
        }
    }
}
