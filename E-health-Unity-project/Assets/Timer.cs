using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 5f;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("music").GetComponent<Music>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            SoundsGameSceneManager.LoadGameScene("Scenes/Game1_3/Game1_3_StartSecondPart");
    }
}
