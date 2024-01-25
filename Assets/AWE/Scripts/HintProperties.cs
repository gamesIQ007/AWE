using UnityEngine;


/// <summary>
/// Свойства подсказки
/// </summary>
[CreateAssetMenu]
public class HintProperties : ScriptableObject
{
    /// <summary>
    /// Заголовок
    /// </summary>
    [TextArea]
    [SerializeField] private string title;
    public string Title => title;

    /// <summary>
    /// Текст
    /// </summary>
    [TextArea]
    [SerializeField] private string hintText;
    public string HintText => hintText;

    /// <summary>
    /// Изображение
    /// </summary>
    [SerializeField] private Sprite hintImage;
    public Sprite HintImage => hintImage;
}