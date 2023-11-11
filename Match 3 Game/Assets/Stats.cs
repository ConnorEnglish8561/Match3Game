using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public static Stats instance;

    public int score, matches;
    public float remainingTime = 120f;
    public Text scoreText, matchesText, timeText;
    public Canvas gameOver;
    public Canvas insructions;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(remainingTime > 0 && score > 0)
            remainingTime -= Time.deltaTime;

        scoreText.text = "Score: " + score.ToString();
        matchesText.text = "Matches: " + matches.ToString();
        timeText.text = "Time: " + remainingTime.ToString("0");

        if (remainingTime <= 0f)
        {
            gameOver.enabled = true;
            //BoardManager.instance.NewBoard();
        }

        if (score == 0)
        {
            insructions.enabled = true;
        }
        else
        {
            insructions.enabled = false;
        }
    }

    public void Reset()
    {
        gameOver.enabled = false;
        score = 0;
        remainingTime = 120f;
        matches = 0;
    }
}
