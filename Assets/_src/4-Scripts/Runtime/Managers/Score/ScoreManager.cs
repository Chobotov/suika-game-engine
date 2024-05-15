using System;
using UnityEngine;

namespace SGEngine.Managers
{
    public class ScoreManager : IScoreManager
    {
        private const string RecordSaveKey = "rcrd_scr";

        private int _score;
        private int _recordScore;
        private bool IsHealPoint => _score % 10 == 0;

        public Action<int> ScoreChanged { get; set; }
        public Action ScoreIncrease { get; set; }
        public Action ScoreDecrease { get; set; }
        public Action<int> RecordScoreChanged { get; set; }
        public Action HealPoint { get; set; }

        public int CurrentScore => _score;
        public int RecordScore => _recordScore;

        public ScoreManager()
        {
            Load();
        }

        private void Load()
        {
            _recordScore = PlayerPrefs.GetInt(RecordSaveKey);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(RecordSaveKey, _recordScore);
        }

        public void Reset()
        {
            _score = 0;   
            ScoreChanged?.Invoke(_score);
        }

        public void IncreaseScore(int value = 1)
        {
            _score += value;

            if (_score > _recordScore)
            {
                _recordScore = _score;
                
                RecordScoreChanged?.Invoke(_recordScore);
                
                Save();
            }

            ScoreChanged?.Invoke(_score);
            ScoreIncrease?.Invoke();

            if (IsHealPoint) HealPoint?.Invoke();
        }

        public void DecreaseScore(int value = 1)
        {
            _score -= value;

            if (_score < 0) _score = 0;
            
            ScoreChanged?.Invoke(_score);
            ScoreDecrease?.Invoke();
        }
    }
}
