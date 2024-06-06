using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : DialogueInteract
{
    public DialogueObject unaccomplishedMinigameDialogue;
    public DialogueObject accomplishedMinigameDialogue;
    public void DialogueChange()
    {
        if(isPlayerInRange)
        {
            if (ScoreManager.Instance.CheckIfMinigameCompleted())
            {
                dialogueObject = accomplishedMinigameDialogue;
                Debug.Log("Assigned new Dialogue Object");
            }

            else
                dialogueObject = unaccomplishedMinigameDialogue;
          
        }
        
   
    }

}
