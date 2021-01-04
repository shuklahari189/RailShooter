using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    Text text;
    int score = 0;

    string Score;

    void Start()
    {
        text = GetComponent<Text>();
        Score = score.ToString();

        text.text = "SCORE = " + Score;
    }

    public void ScoreHit(int scorePerHit)
    {
        score = score + scorePerHit;

        Score = score.ToString();
        text.text = "SCORE = " + Score;
    }
}
