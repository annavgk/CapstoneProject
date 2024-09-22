using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInteractable : Interactable
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public override void Interact()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
