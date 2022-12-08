using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    public void StartSpeech()
    {
        StartCoroutine(LoadSpeech());
    }

    IEnumerator LoadSpeech()
    {
        transition.SetTrigger("StartSpeech");
        yield return new WaitForSeconds(transitionTime);
    }
    
    public void StartTransition()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SoundsGameSceneManager.LoadGameScene(sceneIndex);
    }
}
