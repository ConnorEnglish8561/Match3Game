using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public static Stats instance;

    public float score, matches, highScore, multiplier = 1f, comboTime;
    public float remainingTime = 120f;
    public Text scoreText, matchesText, timeText, highScoreText, multText;
    public Canvas gameOver;
    public Canvas insructions;

    void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (remainingTime > 0 && score > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        scoreText.text = "Score: " + score.ToString("F1");
        matchesText.text = "Matches: " + matches.ToString();
        timeText.text = "Time: " + remainingTime.ToString("0");
        multText.text = multiplier.ToString("F1") + "x   " + comboTime.ToString("F1");

        if (comboTime > 0f && remainingTime > 0f)
        {
            comboTime -= Time.deltaTime * multiplier * 1.5f;
            multiplier -= Time.deltaTime / 10f;
            if (comboTime <= 0f)
            {
                comboTime = 0f;
                multiplier = 1f;
            }

            if (multiplier < 1f)
            {
                multiplier = 1f;
            }
        }

        if (remainingTime <= 0f)
        {
            if (score > highScore)
            {
                highScore = score;
            }
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            gameOver.enabled = true;
            highScoreText.text = "High Score: " + highScore.ToString("F1");

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
        multiplier = 1f;
        comboTime = 0;
    }
}
