using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameObject dialogueSystem = null;
    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [HideInInspector] public bool dialogueOccurring = false;
    private Queue<string> sentences;

    private void Start() 
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (dialogueOccurring && playerScript.inputManager.onSelect)
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();
        dialogueOccurring = true;
        playerScript.inputManager.inputs.Player.Disable();
        playerScript.inputManager.inputs.UI.Enable();
        dialogueSystem.SetActive(true);

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            dialogueOccurring = false;
            playerScript.inputManager.inputs.Player.Enable();
            playerScript.inputManager.inputs.UI.Disable();
            dialogueSystem.SetActive(false);
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
}
