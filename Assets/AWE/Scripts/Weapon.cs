using UnityEngine;


/// <summary>
/// Оружие
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Тип владельца
    /// </summary>
    private enum OwnerType
    {
        /// <summary>
        /// Игрок
        /// </summary>
        Player,
        /// <summary>
        /// Противник
        /// </summary>
        Enemy
    }


    /// <summary>
    /// Тип владельца
    /// </summary>
    private OwnerType ownerType;

    /// <summary>
    /// Свойства оружия. ScriptableObject
    /// </summary>
    [SerializeField] private WeaponProperties weaponProperties;

    /// <summary>
    /// Таймер повторного выстрела
    /// </summary>
    private float refireTimer;

    /// <summary>
    /// Возможность стрельбы
    /// </summary>
    public bool CanFire => refireTimer <= 0;

    /// <summary>
    /// Владелец
    /// </summary>
    private Destructible owner;

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    private Character player;

    /// <summary>
    /// Изображение оружия
    /// </summary>
    private SpriteRenderer sr;

        
    private void Start()
    {
        owner = transform.root.GetComponent<Destructible>();
        player = transform.root.GetComponent<Character>();

        if (player != null)
        {
            ownerType = OwnerType.Player;
        }
        else
        {
            ownerType = OwnerType.Enemy;
        }

        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (refireTimer > 0)
        {
            refireTimer -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Назначить свойства оружия
    /// </summary>
    /// <param name="properties">Свойства оружия</param>
    public void SetProperties(WeaponProperties properties)
    {
        refireTimer = 0;
        sr.sprite = properties.Sprite;
        weaponProperties = properties;
    }

    /// <summary>
    /// Стрельба
    /// </summary>
    public void Fire()
    {
        if (weaponProperties == null) return;
        if (CanFire == false) return;
		if (ownerType == OwnerType.Player && player.Inventory.TryRemoveWeaponAmmo(weaponProperties, weaponProperties.WasteAmmoPerShot) == false) return;

        Projectile projectile = Instantiate(weaponProperties.ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.right;

        if (ownerType == OwnerType.Player)
        {
            projectile.SetProjectileSettings(player, Mathf.RoundToInt(weaponProperties.DamageBase * player.Characteristics.Strenght));
        }
        else
        {
            projectile.SetProjectileSettings(owner, weaponProperties.DamageBase);
        }

        refireTimer = weaponProperties.RateOfFire;

        player.audio.clip = weaponProperties.LaunchSFX;
        player.audio.Play();
    }
}