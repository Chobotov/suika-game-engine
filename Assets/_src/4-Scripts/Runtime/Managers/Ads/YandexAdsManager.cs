using SGEngine.Game;

namespace SGEngine.Managers.Ads
{
    public class YandexAdsManager
    {
        private readonly YandexSDK _yandexSDK;

        public YandexAdsManager()
        {
            _yandexSDK = YandexSDK.instance;

            GameState.GameStateChange += OnGameLose;
        }

        private void OnGameLose(GameState.State state)
        {
            if (state == GameState.State.GameOver)
            {
                OnPlayerLose();
            }
        }

        private void OnPlayerLose()
        {
            #if UNITY_WEBGL

            _yandexSDK.ShowInterstitial();

            #endif
        }
    }
}