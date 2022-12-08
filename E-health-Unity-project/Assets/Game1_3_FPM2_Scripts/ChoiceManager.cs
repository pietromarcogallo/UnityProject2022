using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public void Retry()
    {
        SoundsGameSceneManager.LoadGameScene("Scenes/Game1_3/Game1_3_Presentation");
    }

    public void Quit()
    {
        SoundsGameSceneManager.LoadGameScene("Scenes/MainScene");
    }
}
