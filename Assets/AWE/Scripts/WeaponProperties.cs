using UnityEngine;


[CreateAssetMenu]

/// <summary>
/// Свойства оружия
/// </summary>
public class WeaponProperties : ScriptableObject
{
    /// <summary>
    /// Название оружия
    /// </summary>
    [SerializeField] private string nickname;

    /// <summary>
    /// Спрайт оружия
    /// </summary>
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;

    /// <summary>
    /// Префаб снаряда
    /// </summary>
    [SerializeField] private Projectile projectilePrefab;
    public Projectile ProjectilePrefab => projectilePrefab;

    /// <summary>
    /// Максимальное количество патронов базовое
    /// </summary>
    [SerializeField] private int ammoCountMaxBase;
    public int AmmoCountMaxBase => ammoCountMaxBase;

    /// <summary>
    /// Базовый урон
    /// </summary>
    [SerializeField] private int damageBase;
    public int DamageBase => damageBase;

    /// <summary>
    /// Трата патронов за выстрел
    /// </summary>
    [SerializeField] private int wasteAmmoPerShot;
    public int WasteAmmoPerShot => wasteAmmoPerShot;

    /// <summary>
    /// Скорострельность
    /// </summary>
    [SerializeField] private float rateOfFire;
    public float RateOfFire => rateOfFire;

    /// <summary>
    /// Звук выстрела
    /// </summary>
    [SerializeField] private AudioClip launchSFX;
    public AudioClip LaunchSFX => launchSFX;
}
