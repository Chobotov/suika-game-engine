using SGEngine.Game;
using SGEngine.Runtime.App;
using UnityEngine;

namespace SGEngine.Managers.Audio
{
    public class AudioManager : IAudioManager
    {
        private const string SoundSaveKey = "snd";

        private AudioInitiator _audioInitiator;
        
        private bool _isAudioEnable;

        private void LoadAudioState()
        {
            _isAudioEnable = !PlayerPrefs.HasKey(SoundSaveKey) || PlayerPrefs.GetInt(SoundSaveKey) > 0;
            
            _audioInitiator.SetAudioState(_isAudioEnable);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SoundSaveKey, _isAudioEnable ? 1 : 0);
        }
        
        public AudioManager()
        {
            _audioInitiator = DI.Get<AudioInitiator>();
            
            LoadAudioState();
        }

        public bool IsAudioEnable => _isAudioEnable;

        public void SetAudioState(bool state)
        {
            _isAudioEnable = state;
            
            _audioInitiator.SetAudioState(_isAudioEnable);

            Save();
        }
    }
}