using System.Linq;
using Cysharp.Threading.Tasks;
using SGEngine.Configs.DropItem;
using SGEngine.Runtime.App;
using TapSwap.Utils;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class SpawnItems : MonoBehaviour
    {
        [SerializeField] private DropItemConfig config;
        [Space] 
        [SerializeField] private MergeController mergeController;
        [Space]
        [SerializeField] private DropItem itemPrefab;
        [SerializeField] private Transform spawnPoint;

        private DropItem currentItem;

        public DropItem CurrentItem
        {
            get => currentItem;
            set => currentItem = value;
        }

        private void Start()
        {
            DI.Add(this);

            Spawn();
        }

        [ContextMenu("Spawn")]
        public async void Spawn()
        {
            var data = config.Items.GetRandomItemByWeight(config.Items.Select(x => x.Weight));

            await UniTask.Delay(1000);

            currentItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);

            currentItem.name = data.ID;

            currentItem.Init(data);

            currentItem.OnCollisionDetect += OnCollisionDetect;
            currentItem.OnDestroy += OnItemDestroy;
        }

        private void OnItemDestroy(DropItem item)
        {
            item.OnCollisionDetect -= OnCollisionDetect;
            item.OnDestroy -= OnItemDestroy;
        }

        private void OnCollisionDetect(DropItem firstItem, DropItem secondItem)
        {
            mergeController.TryMerge(firstItem, secondItem);
        }

        public void SpawnItem(string itemId, Vector3 position)
        {
            var itemData = config.Items.FirstOrDefault(x => x.ID.Equals(itemId));

            if (itemData == null)
            {
                Debug.LogError($"Cant find DropItem to spawn with Id : {itemId}!");
            }

            var item = Instantiate(itemPrefab, position, Quaternion.identity);

            item.name = itemData.ID;

            item.Init(itemData);
            item.SetRigidbodyType(RigidbodyType2D.Dynamic);

            item.OnCollisionDetect += OnCollisionDetect;
            item.OnDestroy += OnItemDestroy;
        }
    }
}
