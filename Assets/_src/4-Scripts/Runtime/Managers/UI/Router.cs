using System.Linq;
using SGEngine.App;
using SGEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = SGEngine.UI.Screen;

namespace SGEngine.Managers
{
    public class Router : MonoBehaviour, IRouter
    {
        [SerializeField] private Screen[] _screens;
        [Space]
        [SerializeField] private Image _backGround;
        [Space]
        [SerializeField] private GameObject screenContainer;

        private Screen _currentScreen;

        private void Awake()
        {
            screenContainer.SetActive(false);

            DI.Add<IRouter>(this);
        }
        
        public Screen CurrentScreen => _currentScreen;

        public void Init()
        {
            screenContainer.SetActive(true);
        }

        public void ShowScreen(ScreenType type)
        {
            var screen = _screens.FirstOrDefault(x => x.Type == type);
            
            if (screen == null) return;
            
            _currentScreen = screen;
            screen.Show();
        }

        public void HideScreen(ScreenType type)
        {
            var screen = _screens.FirstOrDefault(x => x.Type == type);
            
            if (screen == null) return;
            
            screen.Hide();
        }

        public void HideCurrentScreen()
        {
            if (!_currentScreen) return;

            HideScreen(_currentScreen.Type);
        }
        
        public void HideGameElements()
        {
            _backGround.enabled = true;
        }

        public void ShowGameElements()
        {
            _backGround.enabled = false;
        }
    }
}