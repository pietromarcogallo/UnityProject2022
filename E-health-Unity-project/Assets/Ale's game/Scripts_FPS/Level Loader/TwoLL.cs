using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoLL : MonoBehaviour
{
    public Animator transition;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("SameShapeDifferentColor"));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

}
