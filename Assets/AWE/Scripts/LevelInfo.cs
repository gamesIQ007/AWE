using UnityEngine;


[CreateAssetMenu]

/// <summary>
/// Информация об уровне
/// </summary>
public class LevelInfo : ScriptableObject
{
    /// <summary>
    /// Имя сцены
    /// </summary>
    [SerializeField] private string sceneName;
    public string SceneName => sceneName;

    /// <summary>
    /// Спрайт
    /// </summary>
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    /// <summary>
    /// Заголовок
    /// </summary>
    [SerializeField] private string title;
    public string Title => title;
}
