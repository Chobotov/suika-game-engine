using SGEngine.App;
using SGEngine.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SGEngine.UI.Screens
{
    public class MainGame : Screen
    {
        [SerializeField] private Text _score;
        [SerializeField] private Text _recordScore;

        private IScoreManager _scoreManager;

        public override ScreenType Type => ScreenType.MainGame;

        private void Start()
        {
            _scoreManager = DI.Get<IScoreManager>();

            _scoreManager.ScoreChanged += OnScoreChanged;
            _scoreManager.RecordScoreChanged += OnRecordScoreChanged;

            SetScoreText(_recordScore, _scoreManager.RecordScore);
        }

        private void OnRecordScoreChanged(int recordScore)
        {
            SetScoreText(_recordScore, recordScore);
        }

        private void OnScoreChanged(int score)
        {
            SetScoreText(_score, score);
        }

        private void SetScoreText(Text textField, int score)
        {
            textField.text = $"{score}";
        }
    }
}