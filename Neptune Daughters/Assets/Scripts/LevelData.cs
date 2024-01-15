using System;

namespace Script
{
    [Serializable]
    public struct LevelData
    {
        public string name;
        public int score;

        public LevelData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    
    
}