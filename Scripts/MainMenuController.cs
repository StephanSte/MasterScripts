using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject loadingUI;
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
