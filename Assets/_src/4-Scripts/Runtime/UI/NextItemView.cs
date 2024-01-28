using SGEngine.Configs.DropItem;
using SGEngine.DropItem;
using UnityEngine;
using UnityEngine.UI;

namespace SGEngine.UI
{
    public class NextItemView : MonoBehaviour
    {
        [SerializeField] private SpawnItems spawnItems;
        [SerializeField] private Image image;
        [SerializeField] private Button refresh;

        private void Awake()
        {
            spawnItems.OnNextItemChanged += ShowNextItem;
            
            refresh.onClick.AddListener(RefreshItem);
        }

        private void RefreshItem()
        {
            spawnItems.RefreshNextItemData();
        }

        private void ShowNextItem(DropItemData data)
        {
            image.sprite = data.Sprite;
        }
    }
}