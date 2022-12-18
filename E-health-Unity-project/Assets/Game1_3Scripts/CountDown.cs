using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CountDown : MonoBehaviour
{
    private float timer = 120f;
    private TextMeshProUGUI timerSeconds;

    public GameObject popup;

    public TransitionManager[] transitions;


    // Use this for initialization
    void Start()
    {
        timerSeconds = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!popup.activeSelf)
            timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f2");
        if (timer <= 0)
        {
            timerSeconds.text = string.Empty;
            SoundsGameScoreManager.SaveScore();
            for (int i = 0; i < transitions.Length; ++i)
            {
                transitions[i].StartTransition();
            }
        }   
    }
}