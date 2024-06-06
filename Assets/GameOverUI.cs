using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text jobTitleText;
    public TMP_Text finalScoreText;

    public void UIEditTitle(Component sender, object data)
    {
        if(data is string title)
        {
            jobTitleText.text = "Your job title: " + title;
        }
        finalScoreText.text = "Final Score: " + ScoreManager.Instance.score;
       

        
    }
}
