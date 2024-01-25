using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ����������� ���������
/// </summary>
public class Hint : MonoBehaviour
{
    /// <summary>
    /// �������� ���������
    /// </summary>
    [SerializeField] private HintProperties properties;

    /// <summary>
    /// ���������
    /// </summary>
    [SerializeField] private Text title;

    /// <summary>
    /// ����� ���������
    /// </summary>
    [SerializeField] private Text description;

    /// <summary>
    /// ��������
    /// </summary>
    [SerializeField] private Image image;


    private void Start()
    {
        title.text = properties.Title;
        description.text = properties.HintText;
        image.sprite = properties.HintImage;
    }


    /// <summary>
    /// �������� ���������
    /// </summary>
    public void ShowHint()
    {
        gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public void CloseHint()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
