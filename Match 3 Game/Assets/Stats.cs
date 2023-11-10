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
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        scoreText.text = "Score: " + score.ToString();
        matchesText.text = "Matches: " + matches.ToString();
        timeText.text = "Time: " + remainingTime.ToString("0");

        if (remainingTime <= 0f)
        {
            BoardManager.instance.NewBoard();
        }
    }

    public void Reset()
    {
        score = 0;
        remainingTime = 120f;
        matches = 0;
    }
}
