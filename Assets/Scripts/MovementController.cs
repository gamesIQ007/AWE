using UnityEngine;

[RequireComponent(typeof(Player))]

/// <summary>
/// Класс для управления игроком
/// </summary>
public class MovementController : MonoBehaviour
{
    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    private Player player;


    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player == null) return;

        ControlKeyboard();
    }


    /// <summary>
    /// Реализация управления с клавиатуры
    /// </summary>
    private void ControlKeyboard()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector2 movementVector = new Vector2(horizontalMovement, verticalMovement);

        if (movementVector.magnitude > 1)
        {
            movementVector = movementVector.normalized;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point = new Vector3(point.x, point.y, 0);

            player.Fire(point);
        }

        player.MovementControl = movementVector;
    }
}
