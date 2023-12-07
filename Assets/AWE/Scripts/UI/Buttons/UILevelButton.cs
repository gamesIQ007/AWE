using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Кнопка уровня
/// </summary>
public class UILevelButton : UISelectableButton, IScriptableObjectProperty
{
    /// <summary>
    /// Информация об уровне
    /// </summary>
    [SerializeField] private LevelInfo levelInfo;
    public LevelInfo LevelInfo => levelInfo;

    /// <summary>
    /// Иконка
    /// </summary>
    [SerializeField] private Image icon;
    /// <summary>
    /// Заголовок
    /// </summary>
    [SerializeField] private Text title;


    private void Start()
    {
        ApplyProperty(levelInfo);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (interactable == false) return;
        if (levelInfo == null) return;

        SceneManager.LoadScene(levelInfo.SceneName);
    }


    /// <summary>
    /// Применить свойства
    /// </summary>
    /// <param name="property">Свойства</param>
    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;
        if (property is LevelInfo == false) return;

        levelInfo = property as LevelInfo;

        icon.sprite = levelInfo.Icon;
        title.text = levelInfo.Title;
    }
}
