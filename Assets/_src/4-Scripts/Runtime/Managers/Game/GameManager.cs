using SGEngine.Game;
using SGEngine.Managers.Audio;
using SGEngine.Managers.Score;
using SGEngine.Managers.UI;
using SGEngine.Runtime.App;
using SGEngine.UI;

namespace SGEngine.Managers.Game
{
    public class GameManager : IGameManager
    {
        private IRouter router;
        private IScoreManager scoreManager;
        private IAudioManager audioManager;

        private AudioInitiator audioInitiator;

        private SGEngine.Game.Game game;

        public GameManager(IRouter router, IScoreManager scoreManager, IAudioManager audioManager)
        {
            this.router = router;
            this.scoreManager = scoreManager;
            this.audioManager = audioManager;

            audioInitiator = DI.Get<AudioInitiator>();
            
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

        public void StartSession()
        {
            Start();
        }

        public void Start()
        {
            router.HideCurrentScreen();
            router.ShowScreen(ScreenType.MainGame);

            game = new SGEngine.Game.Game(scoreManager, audioInitiator);
        }

        public void GameOver()
        {
            router.HideCurrentScreen();
            router.ShowScreen(ScreenType.GameOver);

            router.PlayerInfo.RecordScore.text = $"{scoreManager.RecordScore}";
            router.PlayerInfo.CurrentScore.text = $"{scoreManager.CurrentScore}";

            router.PlayerInfo.Show();
        }
    }
}