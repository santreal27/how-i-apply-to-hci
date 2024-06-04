using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    #region Singleton
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public static MainManager Instance;
    public int score;

    
    public void LoadScene(Component sender, object data)
    {
        if(data is string id)
        {
            SceneManager.LoadScene(id);
        }
        
    }

}
