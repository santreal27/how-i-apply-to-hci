using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameTapping : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private bool startPlaying;

    [SerializeField] private BeatScrolling beatScroller;

    [SerializeField] int correctKeys = 0;
    [SerializeField] int wrongKeys = 0;

    [Header("UI Variables")]
    [SerializeField] private TMP_Text correctScoreText, missedScoreText;
    [SerializeField] private TMP_Text finalCorrectScore, finalMissedScore;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Game Event")]
    public GameEvent goToMainGameEvent;

    int finalScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (!startPlaying)
      {
          if(Input.anyKeyDown)
          {
                startPlaying = true;
                musicSource.Play();
          }
      }
    }

    public void AddScoreKeys(Component sender, object data)
    {
        if((bool)data == true)
        {
            Debug.Log("Add Correct");
            correctKeys++;
            if(correctScoreText != null )
            {
                correctScoreText.text = correctKeys.ToString();
                finalCorrectScore.text = correctKeys.ToString();
            }
                
        }
        else if((bool)data == false)
        {
            Debug.Log("Add Missed");
           wrongKeys++;
           if (missedScoreText != null)
           {
                missedScoreText.text = wrongKeys.ToString();
                finalMissedScore.text = wrongKeys.ToString();
           }

           
        }
        
    }

    public void ConvertScore()
    {
        finalScore = (correctKeys - wrongKeys) * 100;
        finalScoreText.text = "Final Score: " + finalScore.ToString();
        ScoreManager.Instance.AddScore(finalScore);

    }

    public void GoToMainGame()
    {
        goToMainGameEvent.Raise(this, "MainGame");
    }

   
}
