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
            case "score_1": text.text = PlayerPrefs.GetFloat("ScoreGame1") + "/100"; break;
            case "score_2": text.text = PlayerPrefs.GetFloat("ScoreGame2") + "/200"; break;
            case "score_3": text.text = PlayerPrefs.GetFloat("ScoreGame3") + "/200"; break;
        }
    }
}
