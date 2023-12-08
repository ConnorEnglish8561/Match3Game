using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    bool sounds = true;
    public TextMeshProUGUI soundButton;

    public void Start()
    {
        if (AudioListener.pause == true)
        {
            sounds = false;
            soundButton.text = "Muted";
        }
    }
    public void toggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void toggleVolume()
    {
        sounds = !sounds;

        if (sounds)
        {
            AudioListener.pause = false;
            AudioListener.volume = 1;
            soundButton.text = "Toggle Sounds";
        }

        else
        {
            AudioListener.pause = true;
            AudioListener.volume = 0;
            soundButton.text = "Muted";
        }
    }
}
