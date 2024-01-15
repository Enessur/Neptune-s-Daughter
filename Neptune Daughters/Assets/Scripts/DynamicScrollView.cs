using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using TMPro;
public class DynamicScrollView : MonoBehaviour
{
    
    [SerializeField] private ScoreContainer prefab;
    [SerializeField] private Transform scrollViewContent;
    private List<ScoreContainer> _scoreContainers = new ();
    

    private void OnEnable()
    {
        LevelManager.onLevelDataUpdated += UpdateScoreContainers;
        
    }

    private void OnDisable()
    {
        LevelManager.onLevelDataUpdated -= UpdateScoreContainers;
    }

    private void Start()
    {
        InitializePanels();
        UpdateScoreContainers(LevelManager.Instance.levelData);
    }

    private void UpdateScoreContainers(List<LevelData> levelDataList)
    {
        for (int i = 0; i < LevelManager.levelDataLenght; i++)
        {
            _scoreContainers[i].SetValues(levelDataList[i]);
        }
        
    }

    private void InitializePanels()
    {
        for (int i = 0; i < LevelManager.levelDataLenght; i++)
        {
            ScoreContainer scoreContainer = Instantiate(prefab, scrollViewContent);
            _scoreContainers.Add(scoreContainer);
        }
    }

    
     
    
}
