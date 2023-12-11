using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateParticleScore : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    public void updateText(string num)
    {
        scoreText.text = num;
    }
}
