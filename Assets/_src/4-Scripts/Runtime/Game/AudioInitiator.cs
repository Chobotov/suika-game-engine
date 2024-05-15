using SGEngine.App;
using SGEngine.Configs.Audio;
using UnityEngine;

namespace SGEngine.Game
{
    public class AudioInitiator : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [Space]
        [SerializeField] private AudioConfig _audioConfig;
        
        private void Awake()
        {
            DI.Add(this);
        }

        public void SetAudioState(bool isOn)
        {
            _source.mute = !isOn;
        }

        public void PlayCorrect()
        {
            _source.PlayOneShot(_audioConfig.CorrectColors);
        }

        public void PlayIncorrect()
        {
            _source.PlayOneShot(_audioConfig.InCorrectColors);
        }

        public void PlayMerge()
        {
            _source.PlayOneShot(_audioConfig.MergeSfx);
        }

        public void PlayGameOver()
        {
            _source.PlayOneShot(_audioConfig.GameOver);
        }
    }
}