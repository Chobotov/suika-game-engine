using SGEngine.Managers.Game;
using SGEngine.Managers.Score;
using SGEngine.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace SGEngine.UI.Screens
{
    public class MainGame : Screen
    {
        [SerializeField] private Text _score;

        private IScoreManager _scoreManager;
        private IGameManager _gameManager;

        public override ScreenType Type => ScreenType.MainGame;

        private void Start()
        {
            _scoreManager = DI.Get<IScoreManager>();
            _gameManager = DI.Get<IGameManager>();

            _scoreManager.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _score.text = $"{score}";
        }
    }
}