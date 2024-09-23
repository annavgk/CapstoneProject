using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float interactionRange = 1f;
    public float playerScale = 5f;
    public LayerMask pushableLayer;

    private Vector2 movement;
    private Interactable currentInteractable;
    private DialogueManager dialogueManager;

    public bool canUnderstandRats = false;
    public bool lienInParty = false;
    public bool lienReadyToFollow = false;

    private bool isPushing = false;

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
            float localScaleX = Mathf.Sign(movement.x) * playerScale;
            transform.localScale = new Vector3(localScaleX, playerScale, 1f);
        }

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null && !dialogueManager.dialogueActive)
        {
            currentInteractable.Interact();
        }
    }

    void FixedUpdate()
    {
        if (!isPushing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        if (movement != Vector2.zero && !isPushing)
        {
            AttemptPush();
        }
    }

    void AttemptPush()
    {
        Vector2 checkDirection = new Vector2(movement.x, movement.y);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, checkDirection, 1f, pushableLayer);

        if (hit.collider != null)
        {
            PushableObject pushable = hit.collider.GetComponent<PushableObject>();
            if (pushable != null)
            {
                isPushing = true;
                pushable.Push(checkDirection, () => isPushing = false);
            }
        }
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
