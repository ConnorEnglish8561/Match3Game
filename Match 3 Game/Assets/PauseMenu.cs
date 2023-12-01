using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;

    public Canvas canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }

    public void togglePause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            canvas.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            canvas.gameObject.SetActive(false);
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void closeGame()
    {
        Application.Quit();
    }
}