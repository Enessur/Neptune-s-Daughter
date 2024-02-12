using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UpperText : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text lifeText;
    public TMP_Text highScoreText;

    private int _highScore;
    private void Awake()
    {
        LevelManager.onScoreChanged += OnScoreChanged;
       
    }
    
    

    private void OnScoreChanged(int score)
    {
        scoreText.text = $"SCORE : {score}";
        highScoreText.text = $"HIGH-SCORE : {LevelManager.Instance.ReturnHighScore()}";
    }

    private void Start()
    {
        scoreText.text = $"SCORE : {LevelManager.Instance.CurrentLevelData()}";
        highScoreText.text = $"HIGH-SCORE : {LevelManager.Instance.ReturnHighScore()}";
    }
}