using UnityEngine;


/// <summary>
/// Главное меню
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Нажатие на кнопку "Выход"
    /// </summary>
    public void OnButtonExit()
    {
        Application.Quit();
    }
}
