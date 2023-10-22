﻿using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Триггер, срабатывающий попадание в него игрока
/// </summary>
public class Trigger : MonoBehaviour
{
    /// <summary>
    /// Событие при достижении точки
    /// </summary>
    [SerializeField] private UnityEvent activateTrigger;
    public UnityEvent ActivateTrigger => activateTrigger;

    /// <summary>
    /// Одноразовый ли триггер?
    /// </summary>
    [SerializeField] private bool oneUseTrigger = false;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Player>() != null)
        {
            activateTrigger.Invoke();
            if (oneUseTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}