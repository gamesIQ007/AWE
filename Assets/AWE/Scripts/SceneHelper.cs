using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Работа со сценами
/// </summary>
public class SceneHelper : MonoBehaviour
{
    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Загрузить уровень
    /// </summary>
    /// <param name="buildIndex">id уровня</param>
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// Выход
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
