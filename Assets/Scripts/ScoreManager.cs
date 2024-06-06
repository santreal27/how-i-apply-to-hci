using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
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
 
    public static ScoreManager Instance;
    public int score;

    [SerializeField] List<bool> minigameCompleted  = new List<bool>();

    public GameEvent scoreEvalEvent;
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void CompletedMinigame()
    {
        if (!minigameCompleted[0])
            minigameCompleted[0] = true;
        else
            minigameCompleted[1] = true;   
    }

    public void CheckScore()
    {
        if (score >= 3000)
        {
            scoreEvalEvent.Raise(this, "CEO");
            // Highest position Job
        }
        else if (score < 2999 && score > 1000)
        {
            scoreEvalEvent.Raise(this, "Manager");
        }
        else
        {
            scoreEvalEvent.Raise(this, "Janitor");
            // Janitor 
        }
        
    }

    public void ResetMinigame()
    {
        score = 0;
        for (int i = 0; i < minigameCompleted.Count; i++)
        {
            minigameCompleted[i] = false;
        }
    }

    public bool CheckIfMinigameCompleted()
    {
        for (int i = 0; i < minigameCompleted.Count; i++)
        {
            if (!minigameCompleted[i])
                return false;
        }
        return true;
    }
}
