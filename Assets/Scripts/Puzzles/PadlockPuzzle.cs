using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PadlockPuzzle : MonoBehaviour
{
    public TMP_InputField[] digitFields;
    public string correctCode = "1234";
    public TextMeshProUGUI feedbackText;
    public Button submitButton;
    public GameObject padlockPrefab;

    private bool isPadlockActive = false;

    void Start()
    {
        submitButton.onClick.AddListener(CheckCode);

        padlockPrefab.SetActive(false);
    }

    public void EnablePadlock()
    {
        padlockPrefab.SetActive(true);
        isPadlockActive = true;
    }

    public void DisablePadlock()
    {
        padlockPrefab.SetActive(false);
        isPadlockActive = false;
    }

    public void CheckCode()
    {
        if (!isPadlockActive) return;

        string playerInput = "";
        for (int i = 0; i < digitFields.Length; i++)
        {
            playerInput += digitFields[i].text;
        }

        if (playerInput == correctCode)
        {
            Debug.Log("Correct! The lock opens!");
            DisablePadlock();
        }
        else
        {
            Debug.Log("Incorrect! Try again.");
        }
    }
}
