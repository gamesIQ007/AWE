using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CircleCollider2D))]

/// <summary>
/// Снаряд и его поведение
/// </summary>
public class Projectile : Entity
{
    [Header("Свойства снаряда")]

    /// <summary>
    /// Тип снаряда
    /// </summary>
    //[SerializeField] private AmmoType ammoType;
    //public AmmoType AmmoType => ammoType;

    /// <summary>
    /// Скорость снаряда
    /// </summary>
    [SerializeField] private float velocity;

    /// <summary>
    /// Время жизни
    /// </summary>
    [SerializeField] private float lifeTime;

    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private int damage;

    /// <summary>
    /// Префаб посмертного эффекта
    /// </summary>
    //[SerializeField] private ImpactEffect impactEffectPrefab;

    /// <summary>
    /// Таймер
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// Дестрактибл родителя
    /// </summary>
    private Destructible parent;
    public Destructible Parent => parent;

    /// <summary>
    /// Признак самонаведения
    /// </summary>
    [SerializeField] private bool isHoming;

    /// <summary>
    /// Цель самонаведения
    /// </summary>
    private Destructible homingTarget;

    [Header("Урон по области")]
    /// <summary>
    /// Наносит ли урон по области
    /// </summary>
    [SerializeField] private bool isAreaDamage;

    /// <summary>
    /// Радиус урона по области
    /// </summary>
    [SerializeField] private float radius;

    /// <summary>
    /// Список дестрактиблов в зоне поражения
    /// </summary>
    List<Destructible> destructiblesInArea;


    private void Start()
    {
        if (isAreaDamage)
        {
            destructiblesInArea = new List<Destructible>();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        
        float stepLength = velocity * Time.deltaTime;
        Vector2 step;
            
        if (isHoming && homingTarget != null)
        {
            step = (homingTarget.transform.position - transform.position).normalized * stepLength;
        }
        else
        {
            step = transform.up * stepLength;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

        if (hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != parent/* && isAreaDamage == false*/)
            {
                ApplyDamage(dest);
            }

            if (isAreaDamage)
            {
                for (int i = 0; i < destructiblesInArea.Count; i++)
                {
                    ApplyDamage(destructiblesInArea[i]);
                }
            }
                
            OnProjectileLifeEnd(hit.collider, hit.point);
        }

        transform.position += new Vector3(step.x, step.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAreaDamage)
        {
            destructiblesInArea.Add(other.transform.root.GetComponent<Destructible>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isAreaDamage)
        {
            destructiblesInArea.Remove(other.transform.root.GetComponent<Destructible>());
        }
    }

        
    /// <summary>
    /// Установить родителя проджектайла
    /// </summary>
    /// <param name="parent">Родитель</param>
    public void SetParentShooter(Destructible parent)
    {
        this.parent = parent;

        if (isHoming)
        {
            homingTarget = FindNearestDestructibleTarget(parent);
        }
    }


    /// <summary>
    /// Применить урон
    /// </summary>
    /// <param name="destructible">Дестрактибл</param>
    private void ApplyDamage(Destructible destructible)
    {
        destructible.ApplyDamage(damage);

        destructible.GetComponent<KnockBack>().ApplyKnockBack(transform);
    }

    /// <summary>
    /// Действие при конце жизни снаряда
    /// </summary>
    /// <param name="col">Коллайдер</param>
    /// <param name="pos">Позиция</param>
    private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        /*if (impactEffectPrefab)
        {
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        }*/
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Поиск ближайшего врага
    /// </summary>
    /// <param name="destructible">Дестрактибл, запустивший снаряд</param>
    /// <returns>Ближайшая цель</returns>
    private Destructible FindNearestDestructibleTarget(Destructible destructible)
    {
        float maxDistance = float.MaxValue;
        Destructible potencialTarget = null;

        foreach (var dest in FindObjectsOfType<Destructible>())
        {
            if (dest == parent) continue;

            float dist = Vector2.Distance(dest.transform.position, destructible.transform.position);

            if (dist < maxDistance)
            {
                maxDistance = dist;
                potencialTarget = dest;
            }
        }

        return potencialTarget;
    }


#if UNITY_EDITOR
    /// <summary>
    /// Цвет гизмо
    /// </summary>
    private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, radius);
    }

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
#endif

}