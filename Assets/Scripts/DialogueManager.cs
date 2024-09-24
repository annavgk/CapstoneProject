using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;//no more assigning things publicly. This essentially gives everything access.
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    private Queue<string> sentences;
    private bool isTyping;
    private string currentSentence;

    public bool dialogueActive { get; private set; } 

    private float typingSpeed = 0.05f;

    void Start()
    {
        sentences = new Queue<string>();
        isTyping = false;
        dialogueActive = false; 
    }

    void Update()
    {
        //we never want to have our Input.GetKeyDown inside of a function like this.
        //why is that?
        //1. Will activate when game is paused
        //2. Wtf is KeyCode.Space? This means you are locked to the spacebar, and if you want to change buttons, you then need to change code
        //It should only be a button that is pressed.
        //3. If you Input commands all throughout your code, you create spaghetti you then need to keep track of (how many places have Input.GetKeyDown?)
        //4. It does not use the new input system.
        
        if (Input.GetKeyDown(KeyCode.Space) && dialogueActive) 
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                isTyping = false;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        dialogueActive = true; 

        nameText.text = dialogue.name;

        sentences.Clear();
        typingSpeed = dialogue.typingSpeed;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;
        currentSentence = sentence;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        dialogueActive = false;

        FindObjectOfType<PlayerController>().lienReadyToFollow = true;

        FindObjectOfType<LienInteractable>().OnDialogueEnd();
    }


}
