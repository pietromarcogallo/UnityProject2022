using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        switch (gameObject.tag)
        {
            case "score_1":
                float rt1 = PlayerPrefs.GetFloat("Num answers 1") == 0 ? PlayerPrefs.GetFloat("Time 1") : (PlayerPrefs.GetFloat("Time 1") / PlayerPrefs.GetFloat("Num answers 1"));
                text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame1") + "/100\nResponse time: " + rt1.ToString("F") + " s";
                break;
            case "score_2":
                float rt2 = PlayerPrefs.GetFloat("Num answers 2") == 0 ? PlayerPrefs.GetFloat("Time 2") : (PlayerPrefs.GetFloat("Time 2") / PlayerPrefs.GetFloat("Num answers 2"));
                text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame2") + "/200\nResponse time: " + rt2.ToString("F") + " s";
                break;
            case "score_3":
                float rt3 = PlayerPrefs.GetFloat("Num answers 3") == 0 ? PlayerPrefs.GetFloat("Time 3") : (PlayerPrefs.GetFloat("Time 3") / PlayerPrefs.GetFloat("Num answers 3"));
                text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame3") + "/200\nResponse time: " + rt3.ToString("F") + " s";
                break;
        }
    }
}
