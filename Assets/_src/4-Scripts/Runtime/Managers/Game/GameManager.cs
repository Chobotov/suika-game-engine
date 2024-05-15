using SGEngine.App;
using SGEngine.Game;
using SGEngine.UI;

namespace SGEngine.Managers
{
    public class GameManager
    {
        private readonly IRouter router;
        private readonly IScoreManager scoreManager;
        private readonly IAudioManager audioManager;

        private readonly AudioInitiator audioInitiator;
        private readonly SpawnItems spawnItems;

        public GameManager(IRouter router, IScoreManager scoreManager, IAudioManager audioManager)
        {
            this.router = router;
            this.scoreManager = scoreManager;
            this.audioManager = audioManager;

            audioInitiator = DI.Get<AudioInitiator>();
            spawnItems = DI.Get<SpawnItems>();

            GameState.GameStateChange += state =>
            {
                if (state != GameState.State.Game) return; 
                
                StartSession();
            };

            GameState.GameStateChange += state =>
            {
                if (state != GameState.State.GameOver) return; 
                
                GameOver();
            };
        }

        private void StartSession()
        {
            router.HideCurrentScreen();
            router.ShowScreen(ScreenType.MainGame);

            spawnItems.ClearItems();
            spawnItems.Spawn();
        }

        private void GameOver()
        {
            audioInitiator.PlayGameOver();
            scoreManager.Reset();

            router.HideCurrentScreen();
            router.ShowScreen(ScreenType.GameOver);
        }
    }
}