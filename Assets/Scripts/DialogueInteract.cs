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
    [SerializeField] protected DialogueObject dialogueObject;
    
    protected bool optionSelected = false;

    [Header("Game Event")]
    public GameEvent onDialogueFinished;

    List<GameObject> optionGameObjects = new List<GameObject>();
    protected bool isPlayerInRange = false;
    public virtual void StartDialogue()
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
        StopAllCoroutines();
        StartDialogue();
        
    }
  

    IEnumerator DisplayDialogue()
    {
        //Deleting Options
        if (optionGameObjects.Count > 0)
        {
            foreach (var go in optionGameObjects)
            {
                Destroy(go);
            }
        }
       // yield return null;
        dialogueCanvas.gameObject.SetActive(true);
        foreach (var dialogue in dialogueObject.dialogueSegments)
        {

            dialogueText.text = dialogue.dialogueText;

            if (dialogue.dialogueChoices.Count == 0)
            {
                dialogueOptionsContainer.SetActive(false);
                yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
                
                //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
              
            else
            {
                dialogueOptionsContainer.SetActive(true);
                optionSelected = false;
                foreach (var option in dialogue.dialogueChoices)
                {
                    GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, option.followOnDialogue, option.dialogueChoice);

                    //add option game object to track 
                    optionGameObjects.Add(newButton);
                }
                while (!optionSelected)
                {
                    yield return null;
                }

                //open options panel
            }
        }

       
       
        //RaiseEvent
        
        onDialogueFinished.Raise(this, 1);

        if (dialogueObject.gameEvent != null)
        {
            dialogueObject.gameEvent.Raise(this,dialogueObject.gameEventId);
        }

        if(dialogueObject.endDialogue != null)
        {
            dialogueObject = dialogueObject.endDialogue;
        }
     
        dialogueOptionsContainer.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
        
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
