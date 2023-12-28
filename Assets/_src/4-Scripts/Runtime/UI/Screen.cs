using UnityEngine;

namespace SGEngine.UI
{
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        protected virtual void Init() { }

        public abstract ScreenType Type { get; }

        public void Show()
        {
            Init();

            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}