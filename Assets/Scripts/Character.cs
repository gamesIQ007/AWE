using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(AmmoBag))]
[RequireComponent(typeof(PlayerCharacteristics))]
[RequireComponent(typeof(PlayerMeleeWeapon))]
[RequireComponent(typeof(AudioSource))]

/// <summary>
/// Игрок
/// </summary>
public class Character : Destructible
{
    /// <summary>
    /// Список оружия
    /// </summary>
    //[SerializeField] private List<WeaponProperties> weapons;

    /// <summary>
    /// Оружие в руках
    /// </summary>
    private Weapon weapon;

    /// <summary>
    /// Индекс активного оружия
    /// </summary>
    //private int activeWeaponIndex = -1;

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
    /// Оруюжие ближнего боя игрока
    /// </summary>
    private PlayerMeleeWeapon playerMeleeWeapon;
    public PlayerMeleeWeapon PlayerMeleeWeapon => playerMeleeWeapon;
		
    /// <summary>
    /// Сохранённая ссылка на сумку боеприпасов
    /// </summary>
    //private AmmoBag ammoBag;
    //public AmmoBag AmmoBag => ammoBag;

    /// <summary>
    /// Звуки
    /// </summary>
    [HideInInspector] public new AudioSource audio;


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //ammoBag = GetComponent<AmmoBag>();
        audio = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
        //weapons = new List<WeaponProperties>();
        characteristics = GetComponent<PlayerCharacteristics>();
        playerMeleeWeapon = GetComponent<PlayerMeleeWeapon>();

        maxHitPoints = characteristics.Hp;
        currentHitPoints = maxHitPoints;
        ChangeHitPoints?.Invoke();
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

    public void HpCharacteristicChanged()
    {
        int prevMaxHP = maxHitPoints;

        maxHitPoints = characteristics.Hp;
        currentHitPoints += maxHitPoints - prevMaxHP;
        ChangeHitPoints?.Invoke();
    }
}
