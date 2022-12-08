using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CongratsText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        SetText();
    }

    private void SetText()
    {
        int stars = (int) Mathf.Floor(PlayerPrefs.GetFloat("FinalScore") / 100);
        text.text = "You obtained " + stars + " stars. ";
        switch (stars)
        {
            case 0: text.text += "You'll do better next time!"; break;
            case 1: text.text = "You obtained 1 star. That's a start!"; break;
            case 2: text.text += "You're on the right way!"; break;
            case 3: text.text += "Good job!"; break;
            case 4: text.text += "Someone is smart!"; break;
            case 5: text.text += "Wow, you are on fire!!"; break;
            default: text.text += ""; break;
        }
    }
}
