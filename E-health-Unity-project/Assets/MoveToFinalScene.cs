using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToFinalScene : MonoBehaviour
{
    public void MoveForward()
    {
        SoundsGameSceneManager.LoadGameScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
