using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class ScoreContainer : MonoBehaviour
    {

        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text nameText;
        public void SetValues(LevelData levelData)
        {
            scoreText.text = $"*{levelData.score}";
            nameText.text = $"*{levelData.name}";
        }
    }
}