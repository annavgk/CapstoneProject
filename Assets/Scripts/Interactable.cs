using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        // This is where you define what happens when the player interacts with the object
        Debug.Log("Interacted with " + gameObject.name);
    }
}
