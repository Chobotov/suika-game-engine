using UnityEngine;

namespace SGEngine.Configs.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "SGEngine/AudioConfig", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _gameOver;
        [SerializeField] private AudioClip _correctColors;
        [SerializeField] private AudioClip _inCorrectColors;

        public AudioClip GameOver => _gameOver;
        public AudioClip CorrectColors => _correctColors;
        public AudioClip InCorrectColors => _inCorrectColors;
    }
}