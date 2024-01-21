using SGEngine.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SGEngine.UI.Screens
{
    public class GameOver : Screen
    {
        [SerializeField] private Button restart;

        public override ScreenType Type => ScreenType.GameOver;

        private void OnEnable()
        {
            restart.onClick.AddListener(OnRestart);
        }

        private void OnDisable()
        {
            restart.onClick.RemoveAllListeners();
        }

        private void OnRestart()
        {
            GameState.SwitchTo(GameState.State.Game);
        }
    }
}