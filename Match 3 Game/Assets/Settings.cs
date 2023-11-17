using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void toggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
