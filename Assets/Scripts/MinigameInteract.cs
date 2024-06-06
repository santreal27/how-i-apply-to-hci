using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : DialogueInteract
{
    
    [SerializeField] bool isFinished;
    [SerializeField] GameEvent onInteract;
    
     
    public void OnFinishedGame()
    {
        isFinished = true;
    }

    public bool CheckFinishedGame()
    {

        return isFinished;
    }
   
}
