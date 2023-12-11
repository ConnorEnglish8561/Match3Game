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
            board.gameObject.transform.position = new Vector3 (10000, 10000, 10000);
        }
        else
        {
            Time.timeScale = 1;
            canvas.gameObject.SetActive(false);
            board.gameObject.transform.position = new Vector3(-3.5f, -3.5f, 0);
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