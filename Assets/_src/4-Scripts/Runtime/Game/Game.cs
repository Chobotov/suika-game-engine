using SGEngine.Managers.Score;

namespace SGEngine.Game
{
    public class Game
    {
        private IScoreManager _scoreManager;

        private AudioInitiator _audioInitiator;

        private void OnPlayerLose()
        {
            _audioInitiator.PlayGameOver();
            
            EndGame();

            GameState.SwitchTo(GameState.State.GameOver);
        }

        private void OnCircleTouchPipe(bool isColorsEquals)
        {
            if (isColorsEquals)
            {
                _scoreManager.IncreaseScore();
                _audioInitiator.PlayCorrect();
            }
            else
            {
                _audioInitiator.PlayIncorrect();
            }
        }
        
        public Game(IScoreManager scoreManager, AudioInitiator audioInitiator)
        {
            _scoreManager = scoreManager;
            _audioInitiator = audioInitiator;
        }

        public void EndGame()
        {
        }
    }
}