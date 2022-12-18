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
            case "score_1": text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame1") + "/100\nResponse time: " + PlayerPrefs.GetFloat("Time 1").ToString("F") + " s"; break;
            case "score_2": text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame2") + "/200\nResponse time: " + PlayerPrefs.GetFloat("Time 2").ToString("F") + " s"; break;
            case "score_3": text.text = "Score: " + PlayerPrefs.GetFloat("ScoreGame3") + "/200\nResponse time: " + PlayerPrefs.GetFloat("Time 3").ToString("F") + " s"; break;
        }
    }
}
