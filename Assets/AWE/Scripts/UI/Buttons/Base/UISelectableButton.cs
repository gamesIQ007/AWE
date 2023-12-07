using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// Кнопка, которую можно выбрать
/// </summary>
public class UISelectableButton : UIButton
{
    /// <summary>
    /// Изображение-выделение выбранной кнопки
    /// </summary>
    [SerializeField] private Image selectImage;

    /// <summary>
    /// События выбора и снятия выбора
    /// </summary>
    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;


    /// <summary>
    /// Установить фокус
    /// </summary>
    public override void SetFocus()
    {
        base.SetFocus();

        selectImage.enabled = true;
        OnSelect?.Invoke();
    }

    /// <summary>
    /// Снять фокус
    /// </summary>
    public override void SetUnFocus()
    {
        base.SetUnFocus();

        selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }
}