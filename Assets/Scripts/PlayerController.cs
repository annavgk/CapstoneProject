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

    void Update()
    {
        // Handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement to maintain consistent speed
        movement = movement.normalized;

        // Flip the character sprite based on movement direction
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movement.x), 1f, 1f);
        }

        // Handle interaction
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void FixedUpdate()
    {
        // Handle movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is interactable
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Clear the current interactable when exiting its range
        if (collision.GetComponent<Interactable>() == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
