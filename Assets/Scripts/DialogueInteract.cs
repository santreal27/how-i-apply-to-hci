using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueInteract : MonoBehaviour
{
    [Header("UI Variables")] 
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] GameObject dialogueOptionsContainer;
    [SerializeField] Transform dialogueOptionsParent;
    [SerializeField] GameObject dialogueOptionsButtonPrefab;
    [SerializeField] TMP_Text dialogueText;

    [Header("Dialogue Object")]
    [SerializeField] DialogueObject dialogueObject;
        
    protected bool optionSelected = false;

    [Header("Game Event")]
    public GameEvent onDialogueFinished;


    protected bool isPlayerInRange = false;
    public void StartDialogue()
    {
        if (isPlayerInRange)
        {
            Debug.Log("Starting Dialogue");
            StartCoroutine(DisplayDialogue());
        }
   
    }

    public void OptionSelected(DialogueObject selectedOption)
    {
        optionSelected = true;
        dialogueObject = selectedOption;
        StartDialogue();
        
    }
  

    IEnumerator DisplayDialogue()
    {
        yield return null;
        dialogueCanvas.gameObject.SetActive(true);
        foreach(var dialogue in dialogueObject.dialogueSegments)
        {
            dialogueText.text = dialogue.dialogueText;

            if(dialogue.dialogueChoices.Count == 0 )
            {
                dialogueOptionsContainer.SetActive(false);
                yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
            }
              
            else
            {
                dialogueOptionsContainer.SetActive(true); 
                foreach (var option in dialogue.dialogueChoices)
                {
                    GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, option.followOnDialogue, option.dialogueChoice);
                }
                while (!optionSelected)
                {
                    yield return null;
                }

                //open options panel
            }
        }

        dialogueOptionsContainer.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
        onDialogueFinished.Raise(this, 1);

        if (dialogueObject.gameEvent != null)
        {
            dialogueObject.gameEvent.Raise(this,dialogueObject.gameEventId);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
