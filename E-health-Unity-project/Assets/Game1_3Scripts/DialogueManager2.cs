using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager2 : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.02f;

    [SerializeField] private bool playerSpeakingFirst;

    [Header("Dialogue TMP text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI responsePlayerDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] GameObject playerContinueButton;
    [SerializeField] GameObject responsePlayerContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] Animator playerSpeechBubbleAnimator;
    [SerializeField] Animator responsePlayerSpeechBubbleAnimator;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [SerializeField] private string[] responsePlayerDialogueSentences;

    private int playerIndex;
    private int responsePlayerIndex;

    private float speechBubbleAnimationDelay = 0.6f;

    private bool dialogueStarted;

    // Function to start the whole beginning dialogue
    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    private void Update()
    {
        if (playerContinueButton.activeSelf)
            if (Input.GetKeyDown(KeyCode.Return))
                TriggerContinueResponsePlayerDialogue();
        if (responsePlayerContinueButton.activeSelf)
            if (Input.GetKeyDown(KeyCode.Return))
                TriggerContinuePlayerDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (playerSpeakingFirst)
        {
            playerSpeechBubbleAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypePlayerDialogue());
        }
        else
        {
            responsePlayerSpeechBubbleAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypeResponsePlayerDialogue());
        }
    }

    // Functions to type the dialogue
    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeResponsePlayerDialogue()
    {
        foreach (char letter in responsePlayerDialogueSentences[responsePlayerIndex].ToCharArray())
        {
            responsePlayerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        responsePlayerContinueButton.SetActive(true);
    }

    // Functions to continue the dialogue after the beginning catchphrases
    private IEnumerator ContinuePlayerDialogue()
    {
        responsePlayerDialogueText.text = string.Empty;
        responsePlayerSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        playerDialogueText.text = string.Empty;
        playerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        if (playerIndex < playerDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
                playerIndex++;
            else
                dialogueStarted = true;
            playerDialogueText.text = string.Empty;
            StartCoroutine(TypePlayerDialogue());
        }
    }

    private IEnumerator ContinueResponsePlayerDialogue()
    {
        playerDialogueText.text = string.Empty;
        playerSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        responsePlayerDialogueText.text = string.Empty;
        responsePlayerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        if (responsePlayerIndex < responsePlayerDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
                responsePlayerIndex++;
            else
                dialogueStarted = true;
            responsePlayerDialogueText.text = string.Empty;
            StartCoroutine(TypeResponsePlayerDialogue());
        }
    }

    public void TriggerContinuePlayerDialogue()
    {
        responsePlayerContinueButton.SetActive(false);
        if (playerIndex >= playerDialogueSentences.Length - 1)
        {
            responsePlayerDialogueText.text = string.Empty;
            responsePlayerSpeechBubbleAnimator.SetTrigger("Close");
        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }

    }

    public void TriggerContinueResponsePlayerDialogue()
    {
        Debug.Log("sono nella funzione");
        playerContinueButton.SetActive(false);
        Debug.Log("sono nella funzione");
        if (responsePlayerIndex >= responsePlayerDialogueSentences.Length - 1)
        {
            Debug.Log("sono nell'if");
            playerDialogueText.text = string.Empty;
            playerSpeechBubbleAnimator.SetTrigger("Close");
        }
        else
        {
            StartCoroutine(ContinueResponsePlayerDialogue());
        }

    }
}
