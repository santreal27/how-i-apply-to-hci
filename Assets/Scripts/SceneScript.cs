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

}
