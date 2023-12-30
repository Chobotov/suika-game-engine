using SGEngine.Managers.Ads;
using SGEngine.Managers.Audio;
using SGEngine.Managers.Game;
using SGEngine.Managers.Score;
using SGEngine.Managers.UI;
using UnityEngine;

namespace SGEngine.Runtime.App
{
    public sealed class StartPoint : MonoBehaviour
    {
        private IGameManager _gameManager;
        private IAudioManager _audioManager;
        private IScoreManager _scoreManager;

        private YandexAdsManager _yandexAdsManager;

        private void Start()
        {
            Debug.Log("Init Game!");

            InitManagers();

            _gameManager.StartSession();
        }

        private void InitManagers()
        {
            var router = DI.Get<IRouter>();

            _audioManager = new AudioManager();
            DI.Add(_audioManager);

            _scoreManager = new ScoreManager();
            DI.Add(_scoreManager);

            _yandexAdsManager = new YandexAdsManager();
            DI.Add(_yandexAdsManager);

            Debug.Log("Managers Inited!");

            router.Init();

            Debug.Log("Router Inited!");

            _gameManager = new GameManager(router, _scoreManager, _audioManager);
            DI.Add(_gameManager);
        }
    }
}