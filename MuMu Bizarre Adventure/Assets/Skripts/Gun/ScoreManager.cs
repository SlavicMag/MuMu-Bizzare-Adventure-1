using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public Text scoreDisplay;

    private void Update()
    {
        scoreDisplay.text = score.ToString();
    }
    public void Kill(int schet)
    {
        score += schet;
    }
}
