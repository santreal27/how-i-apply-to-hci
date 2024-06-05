using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float elapsedTime;

    [Header("UI Variables")]
    public TMP_Text timerText;
    public TMP_Text finalTimerText;
    public TMP_Text scoreText;
    //UnityEvent
    int score = 0;
    void Start()
    {

    }
    public void StartTimer()
    {
        StartCoroutine(TimerStarting());
    }

    public void StopTimer()
    {
        finalTimerText.text = "Time: " + Mathf.RoundToInt(elapsedTime).ToString();
        AddScore();
        StopAllCoroutines();
    }
    IEnumerator TimerStarting()
    {
        while(true)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Time: " + Mathf.RoundToInt(elapsedTime);
            yield return null;
        }
      
    }

    public void AddScore()
    {

        score = 100000 / Mathf.RoundToInt(elapsedTime);
        scoreText.text = "Score: "+ score.ToString();
        ScoreManager.Instance.AddScore(score);
    }



}
