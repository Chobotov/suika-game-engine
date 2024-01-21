using SGEngine.DropItem;
using SGEngine.Managers.Score;
using SGEngine.Runtime.App;

namespace SGEngine.Game
{
    public class Game
    {
        private IScoreManager scoreManager;

        private SpawnItems spawnItems;
        private AudioInitiator audioInitiator;

        private SpawnItems SpawnItems => spawnItems ??= DI.Get<SpawnItems>();

        public Game(IScoreManager scoreManager, AudioInitiator audioInitiator)
        {
            this.scoreManager = scoreManager;
            this.audioInitiator = audioInitiator;

            SpawnItems.ClearItems();
            SpawnItems.Spawn();
        }

        private void OnCircleTouchPipe(bool isColorsEquals)
        {
            if (isColorsEquals)
            {
                scoreManager.IncreaseScore();
                audioInitiator.PlayCorrect();
            }
            else
            {
                audioInitiator.PlayIncorrect();
            }
        }

        private void OnPlayerLose()
        {
            audioInitiator.PlayGameOver();

            EndGame();

            GameState.SwitchTo(GameState.State.GameOver);
        }

        public void EndGame()
        {
        }
    }
}