using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCharacteristics))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(AudioSource))]

/// <summary>
/// Игрок
/// </summary>
public class Character : Destructible
{
    /// <summary>
    /// Оружие в руках
    /// </summary>
    private Weapon weapon;

    /// <summary>
    /// Индекс активного оружия
    /// </summary>
    private int activeWeaponIndex = -1;

    /// <summary>
    /// Сохранённая ссылка на ригид
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Сохранённая ссылка на характеристики
    /// </summary>
    private PlayerCharacteristics characteristics;
    public PlayerCharacteristics Characteristics => characteristics;

    /// <summary>
    /// Сохранённая ссылка на инвентарь
    /// </summary>
    private Inventory inventory;
    public Inventory Inventory => inventory;

    /// <summary>
    /// Оруюжие ближнего боя игрока
    /// </summary>
    private MeleeAttack meleeAttack;
    public MeleeAttack MeleeAttack => meleeAttack;
		
    /// <summary>
    /// Звуки
    /// </summary>
    [HideInInspector] public new AudioSource audio;


    private void Awake()
    {
        characteristics = GetComponent<PlayerCharacteristics>();
        inventory = GetComponent<Inventory>();
    }

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
        meleeAttack = GetComponent<MeleeAttack>();
        maxHitPoints = characteristics.Hp;
        currentHitPoints = maxHitPoints;
        ChangeHitPoints?.Invoke(0, Vector2.zero);

        weapon.SetProperties(inventory.WeaponsInInventory[0].WeaponProperties);
        activeWeaponIndex = 0;
    }

    private void FixedUpdate()
    {
        UpdateRigitBody();
    }


    /// <summary>
    /// Управление движением
    /// </summary>
    public Vector2 MovementControl { get; set; }

    /// <summary>
    /// Метод добавления сил для движения
    /// </summary>
    private void UpdateRigitBody()
    {
        rb.velocity = new Vector2(MovementControl.x * characteristics.Speed, MovementControl.y * characteristics.Speed);
    }


    /*private void SetActiveWeapon(WeaponProperties properties)
    {
        weapon.SetProperties(weapons[activeWeaponIndex]);
    }*/

    /// <summary>
    /// Переключиться на следующее оружие
    /// </summary>
    public void SwitchOnNextWeapon()
    {
        WeaponProperties properties;
        inventory.ReturnNextWeapon(activeWeaponIndex, out properties, out activeWeaponIndex);
        weapon.SetProperties(properties);
    }

    /// <summary>
    /// Переключиться на предыдущее оружие
    /// </summary>
    public void SwitchOnPrevWeapon()
    {
        WeaponProperties properties;
        inventory.ReturnPrevWeapon(activeWeaponIndex, out properties, out activeWeaponIndex);
        weapon.SetProperties(properties);
    }

    /// <summary>
    /// Стрельба
    /// </summary>
    /// <param name="point">Цель выстрела</param>
    public void Fire(Vector3 point)
    {
        weapon.Fire();
    }

    /// <summary>
    /// Добавить оружие
    /// </summary>
    /// <param name="weaponProperties">Свойства оружия</param>
    /*public void AddWeapon(WeaponProperties weaponProperties)
    {
        weapons.Add(weaponProperties);
        activeWeaponIndex = weapons.IndexOf(weaponProperties);
        SetActiveWeapon(weapons[activeWeaponIndex]);
    }*/

    /// <summary>
    /// Изменение характеристик HP
    /// </summary>
    public void HpCharacteristicChanged()
    {
        int prevMaxHP = maxHitPoints;

        maxHitPoints = characteristics.Hp;
        currentHitPoints += maxHitPoints - prevMaxHP;
        ChangeHitPoints?.Invoke(0, Vector2.zero);
    }
}
