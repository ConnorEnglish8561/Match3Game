using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;

    public Canvas canvas;
    public GameObject board;

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
            board.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            canvas.gameObject.SetActive(false);
            board.gameObject.SetActive(true);
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