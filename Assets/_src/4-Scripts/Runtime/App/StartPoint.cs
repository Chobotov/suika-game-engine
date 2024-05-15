using SGEngine.Game;
using SGEngine.Managers;
using UnityEngine;

namespace SGEngine.App
{
    public sealed class StartPoint : MonoBehaviour
    {
        private IAudioManager _audioManager;
        private IScoreManager _scoreManager;
        private GameManager _gameManager;

        private void Start()
        {
            Debug.Log("Init Game!");

            InitManagers();

            GameState.SwitchTo(GameState.State.Game);
        }

        private void InitManagers()
        {
            _audioManager = new AudioManager();
            DI.Add(_audioManager);

            _scoreManager = new ScoreManager();
            DI.Add(_scoreManager);

            Debug.Log("Managers Inited!");

            var router = DI.Get<IRouter>();
            router.Init();

            Debug.Log("Router Inited!");

            _gameManager = new GameManager(router, _scoreManager, _audioManager);
            DI.Add(_gameManager);
        }
    }
}