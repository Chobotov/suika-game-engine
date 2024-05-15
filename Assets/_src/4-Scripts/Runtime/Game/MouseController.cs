using SGEngine.Configs.DropItem;
using UnityEngine;

namespace SGEngine.Game
{
    public class MouseController : MonoBehaviour
    {
        private const int LMB = 0;

        [SerializeField] private DropItemConfig config;
        [SerializeField] private SpawnItems spawnItems;
        [Space]
        [SerializeField] private BoxCollider2D dropZone;

        private Vector3 startPos;
        private bool isMoving;
        private bool isLmbDown;
        private Camera camera;

        private DropItem CurrentItem
        {
            get => spawnItems.CurrentItem;
            set => spawnItems.CurrentItem = value;
        }

        private void Start()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            if (GameState.CurrentState == GameState.State.GameOver)
            {
                isMoving = false;
                isLmbDown = false;

                return;
            }

            if (Input.GetMouseButtonDown(LMB))
            {
                if (!IsMousePositionInBox()) return;

                if (CurrentItem && !isMoving)
                {
                    startPos = Input.mousePosition;
                    isMoving = true;
                    isLmbDown = true;
                }
            }

            if (CurrentItem && isMoving)
            {
                MoveItem();
            }

            if (isLmbDown && Input.GetMouseButtonUp(LMB))
            {
                if (CurrentItem)
                {
                    ReleaseItem();

                    isMoving = false;

                    spawnItems.Spawn();
                }

                isLmbDown = false;
            }
        }

        private bool IsMousePositionInBox()
        {
            var ray = camera.ScreenToWorldPoint(Input.mousePosition);

            if (dropZone.OverlapPoint(ray))
            {
                return true;
            }

            return false;
        }

        private void MoveItem()
        {
            var mouseDelta = Input.mousePosition - startPos;
            var deltaX = mouseDelta.x * config.DropItemSpeed * Time.deltaTime;
            var pos = camera.ScreenToWorldPoint(Input.mousePosition);
            var newPos = new Vector3(pos.x, CurrentItem.transform.position.y, 0);
            var newPosCheck = camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(deltaX, 0, 0);

            if (!dropZone.OverlapPoint(newPosCheck))
            {
                return;
            }

            CurrentItem.transform.SetPositionAndRotation(newPos, Quaternion.identity);
            startPos = Input.mousePosition;
        }

        private void ReleaseItem()
        {
            CurrentItem.SetRigidbodyType(RigidbodyType2D.Dynamic);
            CurrentItem.IsDropProcess = true;
            CurrentItem = null;
        }
    }
}