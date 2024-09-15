using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
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
