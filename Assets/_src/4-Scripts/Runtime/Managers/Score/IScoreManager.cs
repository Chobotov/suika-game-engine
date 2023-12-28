using System;

namespace SGEngine.Managers.Score
{
    public interface IScoreManager
    {
        Action<int> ScoreChanged { get; set; }
        Action ScoreIncrease { get; set; }
        Action ScoreDecrease { get; set; }
        Action HealPoint { get; set; }
        
        int CurrentScore { get; }
        int RecordScore { get; }
        void Reset();
        void IncreaseScore(int value = 1);
        void DecreaseScore(int value = 1);
    }
}