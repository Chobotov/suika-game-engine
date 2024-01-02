using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SGEngine.Game;
using TMPro;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class LimitController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private int gameOverTimer;

        private List<DropItem> itemsInZone = new();

        private bool isGameOver;

        private void Awake()
        {
            SetTimerVisible(false);

            spriteRenderer.DOFade(0f, 0f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<DropItem>(out var dropItem))
            {
                if (!dropItem.IsDropped) return;

                if (!itemsInZone.Contains(dropItem))
                {
                    itemsInZone.Add(dropItem);
                }

                if (isGameOver) return;

                spriteRenderer.DOFade(.8f, .15f);

                StartTimer();

                isGameOver = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent<DropItem>(out var dropItem))
            {
                if (itemsInZone.Contains(dropItem))
                {
                    itemsInZone.Remove(dropItem);
                }

                if (itemsInZone.Count == 0)
                {
                    spriteRenderer.DOFade(0f, .15f);
                    
                    StopTimer();
                }
            }
        }

        private void StopTimer()
        {
            
        }

        private async void StartTimer()
        {
            var currentTime = gameOverTimer;

            text.text = $"{currentTime}";

            SetTimerVisible(true);

            while (currentTime >= 0)
            {
                await UniTask.Delay(1000);

                currentTime--;

                text.text = $"{currentTime}";
            }

            SetTimerVisible(false);

            isGameOver = false;

            GameState.SwitchTo(GameState.State.GameOver);
        }

        private void SetTimerVisible(bool isVisible)
        {
            text.gameObject.SetActive(isVisible);
        }
    }
}