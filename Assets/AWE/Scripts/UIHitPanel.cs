using UnityEngine;


/// <summary>
/// Панель, на которой отображаются всплывающие сообщения о нанесении урона
/// </summary>
public class UIHitPanel : MonoBehaviour
{
    /// <summary>
    /// Объект, на котором будут спавниться всплывашки
    /// </summary>
    [SerializeField] private Transform spawnPanel;

    /// <summary>
    /// Префаб всплывающего сообщения
    /// </summary>
    [SerializeField] private UIHitPopup hitPopup;


    private void Start()
    {
        Destructible[] allDestructibles = FindObjectsOfType<Destructible>();
        for (int i = 0; i < allDestructibles.Length; i++)
        {
            allDestructibles[i].ChangeHitPoints.AddListener(OnChangeHitPoints);
        }
    }

    private void OnDestroy()
    {
        Destructible[] allDestructibles = FindObjectsOfType<Destructible>();
        for (int i = 0; i < allDestructibles.Length; i++)
        {
            allDestructibles[i].ChangeHitPoints.RemoveListener(OnChangeHitPoints);
        }
    }


    /// <summary>
    /// При изменении здоровья
    /// </summary>
    /// <param name="damage"></param>
    private void OnChangeHitPoints(int damage, Vector2 position)
    {
        if (damage <= 0) return;

        UIHitPopup popup = Instantiate(hitPopup);
        popup.transform.SetParent(spawnPanel);
        popup.transform.localScale = Vector3.one;
        popup.transform.position = Camera.main.WorldToScreenPoint(position);

        popup.SetDamageResult(damage);
    }
}
