using SGEngine.Game;
using SGEngine.Managers.Audio;
using SGEngine.Managers.Score;
using SGEngine.Managers.UI;
using SGEngine.Runtime.App;
using SGEngine.UI;
using UnityEngine;

namespace SGEngine.Managers.Game
{
    public class GameManager : IGameManager
    {
        private IRouter _router;
        private IScoreManager _scoreManager;
        private IAudioManager _audioManager;

        private AudioInitiator _audioInitiator;

        private SGEngine.Game.Game _game;

        public GameManager(IRouter router, IScoreManager scoreManager, IAudioManager audioManager)
        {
            _router = router;
            _scoreManager = scoreManager;
            _audioManager = audioManager;

            _audioInitiator = DI.Get<AudioInitiator>();

            GameState.GameStateChange += state =>
            {
                if (state != GameState.State.GameOver) return; 
                
                GameOver();
            };
        }

        public void StartSession()
        {
            Start();
        }

        public void Start()
        {
            _router.HideCurrentScreen();
            _router.ShowGameElements();
            _router.ShowScreen(ScreenType.MainGame);

            GameState.SwitchTo(GameState.State.Game);

            _game = new SGEngine.Game.Game(_scoreManager, _audioInitiator);
        }

        public void GameOver()
        {
            _router.HideCurrentScreen();
            _router.HideGameElements();
            _router.ShowScreen(ScreenType.GameOver);

            _router.PlayerInfo.RecordScore.text = $"{_scoreManager.RecordScore}";
            _router.PlayerInfo.CurrentScore.text = $"{_scoreManager.CurrentScore}";
            _router.PlayerInfo.Show();
        }

        public void Exit()
        {
            PlayerPrefs.Save();
        }
    }
}