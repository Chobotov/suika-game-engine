using System.Collections;
using System.Collections.Generic;
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
        [Space]
        [SerializeField] private bool isTimerProcess;
        [SerializeField] private bool isTimerOver;

        private readonly List<DropItem> itemsInZone = new();

        private void Awake()
        {
            SetTimerVisible(false);

            spriteRenderer.DOFade(0f, 0f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent<DropItem>(out var dropItem)) return;

            if (dropItem.IsDropProcess) return;

            if (!itemsInZone.Contains(dropItem))
            {
                itemsInZone.Add(dropItem);
            }

            if (isTimerProcess || isTimerOver) return;

            spriteRenderer.DOFade(.8f, .15f);

            StartTimer();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.TryGetComponent<DropItem>(out var dropItem)) return;

            if (itemsInZone.Contains(dropItem))
            {
                itemsInZone.Remove(dropItem);
            }

            if (itemsInZone.Count != 0) return;

            spriteRenderer.DOFade(0f, .15f);

            StopTimer();
        }

        private void StartTimer()
        {
            StartCoroutine(StartTimerCoroutine());
        }

        private void StopTimer()
        {
            isTimerProcess = false;
            isTimerOver = false;

            StopAllCoroutines();

            SetTimerVisible(false);
        }

        private IEnumerator StartTimerCoroutine()
        {
            isTimerProcess = true;

            var currentTime = gameOverTimer;

            text.text = $"{currentTime}";

            SetTimerVisible(true);

            while (currentTime >= 0)
            {
                yield return new WaitForSeconds(1);

                currentTime--;

                text.text = $"{currentTime}";
            }

            SetTimerVisible(false);

            isTimerProcess = false;
            isTimerOver = true;

            GameState.SwitchTo(GameState.State.GameOver);
        }

        private void SetTimerVisible(bool isVisible)
        {
            text.gameObject.SetActive(isVisible);
        }
    }
}