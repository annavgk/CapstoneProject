using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInteractable : Interactable
{
    public Dialogue squeakDialogue;
    public Dialogue understandableDialogue;

    public override void Interact()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player.canUnderstandRats)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(understandableDialogue);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(squeakDialogue);
        }
    }
}
