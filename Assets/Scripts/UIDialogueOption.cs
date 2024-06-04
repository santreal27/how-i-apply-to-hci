using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogueOption : MonoBehaviour
{
    DialogueInteract dialogueInteract;

    DialogueObject dialogueObject;

    [SerializeField] TMP_Text dialogueText;

    GameEvent gameEvent;
    public void Setup(DialogueInteract _dialogueInteract, DialogueObject _dialogueObject, string _dialogueText, GameEvent _gameEvent)
    {
        dialogueInteract = _dialogueInteract;
        dialogueObject = _dialogueObject;
        dialogueText.text = _dialogueText;
        gameEvent = _gameEvent;
    }

    public void SelectOption()
    {
        dialogueInteract.OptionSelected(dialogueObject , gameEvent);
    }
}
