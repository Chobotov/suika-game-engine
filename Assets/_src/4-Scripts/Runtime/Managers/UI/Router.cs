using System.Linq;
using SGEngine.Runtime.App;
using SGEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = SGEngine.UI.Screen;

namespace SGEngine.Managers.UI
{
    public class Router : MonoBehaviour, IRouter
    {
        [SerializeField] private Screen[] _screens;
        [SerializeField] private GameObject screenContainer;
        [Space]
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private Image _backGround;

        private Screen _currentScreen;

        private void Awake()
        {
            screenContainer.SetActive(false);
            
            DI.Add<IRouter>(this);
        }
        
        public Screen CurrentScreen => _currentScreen;

        public PlayerInfo PlayerInfo => _playerInfo;

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