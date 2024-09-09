using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float interactionRange = 1f;

    private Vector2 movement;
    private Interactable currentInteractable;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        if (movement.x != 0)
        {
            float localScaleX = Mathf.Sign(movement.x) * 5f;  
            transform.localScale = new Vector3(localScaleX, 5f, 1f); 
        }

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null && !dialogueManager.dialogueActive)
        {
            currentInteractable.Interact();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
