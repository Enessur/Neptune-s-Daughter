using System;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class LevelManager : Singleton<LevelManager>, ISaveData
{
    [SerializeField] private LevelData currentLevelData;
    [SerializeField] private bool toggle;
    [SerializeField] private int startLife;
    [SerializeField] private int playerLife;
    
    public List<LevelData> levelData = new();
    public static int levelDataLenght = 10;

    public static Action<List<LevelData>> onLevelDataUpdated ;
    public static Action<int> onScoreChanged ;

    
    private void Update()
    {
        if (!toggle)
        {
            return;
        }

        toggle = false;
       // AddCurrentLevelData();
    }

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void Start()
    {
        playerLife = startLife;
        if (levelData.Count == 0 || levelData.Count < levelDataLenght)
        {
            for (int i = levelData.Count; i < levelDataLenght; i++)
            {
                levelData.Add(new LevelData("---", 0));
            }
            onLevelDataUpdated?.Invoke(levelData);
        }
    }

    public int GetPlayerLife()
    {
        return playerLife;
    }
    

    public void AddCurrentLevelData(string name)
    {
       
        
        if (levelData.Count < levelDataLenght)
        {
            levelData.Add(currentLevelData);
            SortLevelData();

            SaveData();

            return;
        }

        var last = levelData.LastOrDefault();
        if (last.score >= currentLevelData.score)
        {
            return;
        }

        currentLevelData.name = name;
        LevelData ld = new LevelData(currentLevelData.name, currentLevelData.score);
        levelData.Remove(levelData.LastOrDefault());
        levelData.Add(ld);
        SortLevelData();
        SaveData();
    }

    private void SortLevelData()
    {
        levelData.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void AddScore(int score)
    {
        currentLevelData.score += score;
        onScoreChanged?.Invoke(currentLevelData.score);
        Debug.Log("Score Added :"+currentLevelData.score);
    }
    
    public void ResetScore()
    {
        currentLevelData.score = 0;
        onScoreChanged?.Invoke(currentLevelData.score); 
    }
    
    public string SaveKey()
    {
        return "LevelData";
    }

    public void SaveData()
    {
        ES3.Save(SaveKey(), levelData);
        onLevelDataUpdated?.Invoke(levelData);
    }

    public void LoadData()
    {
        levelData = ES3.Load(SaveKey(), levelData);
    }

    public string CurrentLevelData()
    {
        return currentLevelData.score.ToString();
    }

    public int GetHighestScore()
    {
        if (levelData.Count > 0)
        {
            return levelData.Max(data => data.score);
        }
        return 0;
    }
    
    public string ReturnHighScore()
    {
        int highestScore = GetHighestScore();
        return highestScore.ToString();
    }
    
}