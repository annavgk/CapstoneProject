using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPadlock : MonoBehaviour
{
    public PadlockPuzzle padlockPuzzle;

    void Start()
    {
        padlockPuzzle.DisablePadlock();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //no more comparing tags. Use GetComponent. Remembering tags is an exercise in frustration.
        {
            padlockPuzzle.EnablePadlock();
        }
    }
}

