using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsGameSceneManager : MonoBehaviour
{
    public static SoundsGameSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public static void LoadGameScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void LoadGameScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
