using UnityEngine;

[RequireComponent(typeof(Character))]

/// <summary>
/// Класс для управления игроком
/// </summary>
public class MovementController : MonoBehaviour
{
    /// <summary>
    /// Частота смены оружия
    /// </summary>
    [SerializeField] private float changeWeaponTime;

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    private Character player;

    private float changeWeaponTimer = 0;


    private void Start()
    {
        player = GetComponent<Character>();
    }

    private void Update()
    {
        if (player == null) return;

        if (changeWeaponTimer > 0)
        {
            changeWeaponTimer -= Time.deltaTime;
        }

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

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point = new Vector3(point.x, point.y, 0);

            player.MeleeAttack.Attack(point);
        }

        if (changeWeaponTimer <= 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                player.SwitchOnNextWeapon();
                changeWeaponTimer = changeWeaponTime;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                player.SwitchOnPrevWeapon();
                changeWeaponTimer = changeWeaponTime;
            }
        }

        player.MovementControl = movementVector;
    }
}
