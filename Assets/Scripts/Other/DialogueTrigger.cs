using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager = null;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
