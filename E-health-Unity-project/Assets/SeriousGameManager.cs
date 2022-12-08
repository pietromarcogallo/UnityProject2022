using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeriousGameManager : MonoBehaviour
{
    public GameObject settingsPopup;
    public Slider clickSlider;
    public Slider musicSlider;
    public AudioMixer audioMixer;
    public AudioSource clickSound;

    private void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) == true && SceneManager.GetActiveScene().name == "MainScene")
        {
            clickSound.Play();
        }
    }

    public void exitgame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    public void MovetoScene(int SceneID)
    {
        Debug.Log("Moving to Scene: " + SceneID);
        SceneManager.LoadScene(SceneID);
    }

    public void toggleSettingsPopup()
    {
        settingsPopup.SetActive(!settingsPopup.activeSelf);
    }

    public void closeSettingsPopup()
    {
        settingsPopup.SetActive(false);
    }


    public void ChangeVolume()
    {
        if( musicSlider.value <= - 48)
        {
            audioMixer.SetFloat("MusicVol", -80);
        }
        else
        {
            audioMixer.SetFloat("MusicVol", musicSlider.value);
        }
    }

    public void clickSliderValueChange()
    {
        if (clickSlider.value <= -48)
        {
            audioMixer.SetFloat("SoundEffectsVol", -80);
        }
        else
        {
            audioMixer.SetFloat("SoundEffectsVol", clickSlider.value);
        }
    }
  
}

