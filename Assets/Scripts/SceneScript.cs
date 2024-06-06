using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void LoadNextScene(Component sender , object data)
    {
        if (data is string sceneName && sceneName != "")
            SceneManager.LoadScene(sceneName);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
        ScoreManager.Instance.ResetMinigame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
