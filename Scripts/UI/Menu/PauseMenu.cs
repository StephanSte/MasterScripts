using UnityEngine;
using UnityEngine.SceneManagement;


namespace Menu
{
    
    public class PauseMenu: MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject PauseGameMenuUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            PauseGameMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Game should play again");
        }

        void PauseGame()
        {
            PauseGameMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("Pausing");
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            Debug.Log("Going to main menu");
            SceneManager.LoadScene("Main Menu");
        }
        
        public void QuitGame()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }
}