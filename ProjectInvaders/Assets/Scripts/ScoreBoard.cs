using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;
    
    void Start() {
        scoreText = GetComponent<TMP_Text>();
    }

    public void IncreaseScore(int incremental){
        score += incremental;
        scoreText.text = $"Score : {score}".ToString();
        Debug.Log(score);
    }
}
