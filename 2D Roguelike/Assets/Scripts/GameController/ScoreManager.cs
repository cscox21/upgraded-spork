using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;

    Text scoreValue;

    private void Start()
    {
        scoreValue = GetComponent<Text>();
        score = 0;
    }

    private void Update()
    {
        if(score<=0)
        
            score = 0;
            scoreValue.text = "" + score;
    }

    public static void AddPoints (int pointsToAdd)
    {
        score += pointsToAdd;
    }

    public static void Reset()
    {
        score = 0;
    }
}
