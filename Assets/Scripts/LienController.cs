using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LienController : MonoBehaviour
{
    public float followDistance = 2f;
    public float stopDistance = 0.5f;
    public float lienSpeed = 3.5f;
    public float followDelay = 5f;
    public Transform playerTransform;

    private PlayerController playerController;
    private Rigidbody2D rb;
    public bool lienCollidingWithPlayer = false;
    private bool isWaiting = false; 
    private Collider2D lienCollider;
    private bool hasInteracted = false; 

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>(); 
        lienCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerController.lienInParty && !lienCollidingWithPlayer && !isWaiting) //we now have three things to check...what if we have 12 things? This is why we use states in our code
        {
            FollowPlayer();
        }
        else if (rb != null && hasInteracted && !isWaiting)
        {
            rb.simulated = false;
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > followDistance)
        {
            lienCollider.enabled = true; 
            rb.simulated = true; 
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, lienSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer < stopDistance)
        {
            StartCoroutine(WaitBeforeFollowing());
        }
    }

    private IEnumerator WaitBeforeFollowing()
    {
        isWaiting = true;
        lienCollider.enabled = false; 
        rb.simulated = false; 
        yield return new WaitForSeconds(followDelay); 
        lienCollider.enabled = true; 
        rb.simulated = true; 
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            lienCollidingWithPlayer = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            lienCollidingWithPlayer = false;
        }
    }

    public void MarkAsInteracted()
    {
        hasInteracted = true;
    }
}
