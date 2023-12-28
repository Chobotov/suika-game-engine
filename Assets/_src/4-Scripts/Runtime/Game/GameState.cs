using System;

namespace SGEngine.Game
{
    public static class GameState
    {
        public static State CurrentState { get; private set; }
        
        public static Action<State> GameStateChange { get; set; }

        public static void SwitchTo(State state)
        {
            CurrentState = state;
            GameStateChange?.Invoke(CurrentState);
        }

        public enum State
        {
            Game,
            Pause,
            GameOver
        }
    }
}