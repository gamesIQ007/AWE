using UnityEngine;

/// <summary>
/// Оружие
/// </summary>
public class Weapon : MonoBehaviour
{
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
    /// Ссылка на игрока
    /// </summary>
    private Player player;

    /// <summary>
    /// Изображение оружия
    /// </summary>
    private SpriteRenderer sr;

        
    private void Start()
    {
        player = transform.root.GetComponent<Player>();
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
		//if (player.AmmoBag.TryTakeAmmo(weaponProperties.ProjectilePrefab.AmmoType, 1) == false) return;

        Projectile projectile = Instantiate(weaponProperties.ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.right;
        projectile.SetParentShooter(player);

        refireTimer = weaponProperties.RateOfFire;

        player.audio.clip = weaponProperties.LaunchSFX;
        player.audio.Play();
    }
}