using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LienInteractable : Interactable
{
    public Dialogue dialogue;
    private PlayerController playerController;
    private LienController lienController;
    private bool hasInteracted = false;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        lienController = FindObjectOfType<LienController>();
    }

    public override void Interact()
    {
        if (!hasInteracted)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

            playerController.lienInParty = true;
            playerController.canUnderstandRats = true;

            playerController.lienReadyToFollow = false;

            hasInteracted = true; 
        }
    }

    public void OnDialogueEnd()
    {
        lienController.MarkAsInteracted(); 
    }
}
