namespace SGEngine.Managers
{
    public interface IAudioManager
    {
        bool IsAudioEnable { get; }
        void SetAudioState(bool state);
    }
}