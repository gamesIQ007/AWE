using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Всплывающая подсказка
/// </summary>
public class Hint : MonoBehaviour
{
    /// <summary>
    /// Свойства подсказки
    /// </summary>
    [SerializeField] private HintProperties properties;

    /// <summary>
    /// Заголовок
    /// </summary>
    [SerializeField] private Text title;

    /// <summary>
    /// Текст подсказки
    /// </summary>
    [SerializeField] private Text description;

    /// <summary>
    /// Картинка
    /// </summary>
    [SerializeField] private Image image;


    private void Start()
    {
        title.text = properties.Title;
        description.text = properties.HintText;
        image.sprite = properties.HintImage;
    }


    /// <summary>
    /// Показать подсказку
    /// </summary>
    public void ShowHint()
    {
        gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    /// <summary>
    /// Закрыть подсказку
    /// </summary>
    public void CloseHint()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
