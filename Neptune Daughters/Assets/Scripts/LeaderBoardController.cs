using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class LeaderBoardController:Singleton<LeaderBoardController>
    {
        [SerializeField] private ScoreContainer scoreContainerPrefab; 
        private List<ScoreContainer> _scoreContainer = new();
        
        protected override void Awake()
        {
            base.Awake();
            Init();
            
        }
        private void Init()
        {
            for (int i = 0; i <LevelManager.levelDataLenght; i++)
            {
                _scoreContainer.Add(Instantiate(scoreContainerPrefab,transform));
            }
            LevelManager.onLevelDataUpdated += UpdateLeaderBoard;
        }

        private void UpdateLeaderBoard(List<LevelData> levelDataList)
        {
            
        }
        
    }
}