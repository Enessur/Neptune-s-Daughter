using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private int score;
    public List<int> scores = new List<int>();
    private int life;


    // public void AddScore(int add)
    // {
    //     score += add;
    // }
    //
    // public void AddScoreList(int score)
    // {
    //     scores.Add(score);
    //     scores.Sort((a, b) => b.CompareTo(a)); // Skorları büyükten küçüğe sırala
    //     SaveScores();
    // }
    //
    // void SaveScores()
    // {
    //     ES3.Save("scores", scores);
    // }
    //
    // public void LoadScores()
    // {
    //     if (ES3.KeyExists("scores"))
    //     {
    //         scores = ES3.Load<List<int>>("scores");
    //     }
    // }

    // public void RemoveLife()
    // {
    //     life--;
    //     ES3.Save<int>("PlayerLife", life);
    //     if (life <= 0)
    //     {
    //         //GAME OVER
    //         AddScoreList(score);
    //     }
    // }
    //
    // public void AddLife()
    // {
    //     life = +3;
    // }
    //
    // public int PrintLife()
    // {
    //     return life;
    // }
    //
    //
    // public int PrintScore()
    // {
    //     return score;
    // }

    // public void SaveScore()
    // {
    //     ES3.Save<int>("PlayerScore", score);
    //     Debug.Log("Score Saved" + score);
    // }
    //
    // public void LoadScore()
    // {
    //     score = ES3.Load<int>("PlayerScore");
    // }

    // public void LoadLife()
    // {
    //     life = ES3.Load<int>("PlayerLife");
    // }
    //
    // public int GetHighestScore(List<int> scores)
    // {
    //     if (scores == null || scores.Count == 0)
    //     {
    //         return 0;
    //     }
    //
    //     scores.Sort((a, b) => b.CompareTo(a));
    //     return scores[0];
    // }
}