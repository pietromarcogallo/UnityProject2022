using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float maxDistance;
    public LayerMask interactableLayers;

    [Header("Buttons (first part)")]
    public Button highButton;
    public Button lowButton;

    [Header("Buttons (second part)")]
    public Button chickenButton;
    public Button cowButton;
    public Button duckButton;
    public Button pigButton;
    public Button sheepButton;
    public Button nullButton;
    public Button[] buttons;

    private Interactable currentInteractable;

    private string sceneName;

    private int firstButtonToAppear, secondButtonToAppear, thirdButtonToAppear;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        buttons = new Button[6] { chickenButton, cowButton, duckButton, pigButton, sheepButton, nullButton };
        // int correctAnswer = ParseAnswer(currentInteractable.GetComponent<AnimalPlaysClip>().GetCorrectAnswer());
        firstButtonToAppear = Random.Range(0, 6);
        secondButtonToAppear = Random.Range(0, 6);
        while (secondButtonToAppear == firstButtonToAppear)
        {
            secondButtonToAppear = Random.Range(0, 6);
        }
        thirdButtonToAppear = Random.Range(0, 6);
        while (thirdButtonToAppear == secondButtonToAppear || thirdButtonToAppear == firstButtonToAppear)
        {
            thirdButtonToAppear = Random.Range(0, 6);
        }
    }

    string ParseAnswerFromIntToString(int answer)
    {
        switch (answer)
        {
            case 0: return "chicken";
            case 1: return "cow";
            case 2: return "duck";
            case 3: return "pig";
            case 4: return "sheep";
            case 5: return "null";
            default: return "avadakedavra";
        }
    }

    int ParseAnswertFromStringToInt(string answer)
    {
        switch (answer)
        {
            case "chicken": return 0;
            case "cow": return 1;
            case "duck": return 2;
            case "pig": return 3;
            case "sheep": return 4;
            case "null": return 5;
            default: return -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactableLayers))
            currentInteractable = hit.collider.GetComponent<Interactable>();
        else
            currentInteractable = null;

        if (sceneName == "Game1_3_FPM")
        {
            highButton.gameObject.SetActive(currentInteractable != null);
            lowButton.gameObject.SetActive(currentInteractable != null);
        }
        else
        {
            /*
            chickenButton.gameObject.SetActive(currentInteractable != null);
            cowButton.gameObject.SetActive(currentInteractable != null);
            duckButton.gameObject.SetActive(currentInteractable != null);
            pigButton.gameObject.SetActive(currentInteractable != null);
            sheepButton.gameObject.SetActive(currentInteractable != null);
            */

            if (currentInteractable != null)
            {
                int correctAnswer;
                if (sceneName == "Game1_3_FPM2")
                    correctAnswer = ParseAnswertFromStringToInt(currentInteractable.GetCorrectAnimalAnswer(visual: false));
                else
                    correctAnswer = ParseAnswertFromStringToInt(currentInteractable.GetCorrectAnimalAnswer(visual: true));
                // Limit case: none of the appearing icon is the cross, but the answer isn't any of the appearing icons
                if (firstButtonToAppear != 5 && secondButtonToAppear != 5 && thirdButtonToAppear != 5)
                    if (correctAnswer != firstButtonToAppear && correctAnswer != secondButtonToAppear && correctAnswer != thirdButtonToAppear)
                        thirdButtonToAppear = 5;
            }
            buttons[firstButtonToAppear].gameObject.SetActive(currentInteractable != null);
            buttons[secondButtonToAppear].gameObject.SetActive(currentInteractable != null);
            buttons[thirdButtonToAppear].gameObject.SetActive(currentInteractable != null);
            // Make the three choices appear where they should be
            buttons[firstButtonToAppear].transform.localPosition = new Vector3(0, -150f, 0);
            buttons[secondButtonToAppear].transform.localPosition = new Vector3(0, 0, 0);
            buttons[thirdButtonToAppear].transform.localPosition = new Vector3(0, 150f, 0);
            // All other buttons mustn't appear
            for (int i = 0; i < buttons.Length; ++i)
                if (i != firstButtonToAppear && i != secondButtonToAppear && i != thirdButtonToAppear)
                    buttons[i].gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        // Debug.Log("is high button: " + highChoice);
        if (currentInteractable != null)
            if (sceneName == "Game1_3_FPM")
                currentInteractable.OnInteraction(EventSystem.current.currentSelectedGameObject.name == "HighButton");
            else if (sceneName == "Game1_3_FPM2")
                currentInteractable.OnInteraction(EventSystem.current.currentSelectedGameObject.name, visual: false);
            else
                currentInteractable.OnInteraction(EventSystem.current.currentSelectedGameObject.name, visual: true);
    }

    public string[] GetButtons()
    {
        return new string[3] {
            ParseAnswerFromIntToString(firstButtonToAppear),
            ParseAnswerFromIntToString(secondButtonToAppear),
            ParseAnswerFromIntToString(thirdButtonToAppear)
        };
    }
}
