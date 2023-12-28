namespace SGEngine.Managers.Ads
{
    public class YandexAdsManager
    {
        private YandexSDK _yandexSDK;

        public YandexAdsManager()
        {
            _yandexSDK = YandexSDK.instance;
        }

        private void OnPlayerLose()
        {
            _yandexSDK.ShowInterstitial();
        }
    }
}