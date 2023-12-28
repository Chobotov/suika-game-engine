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
        [SerializeField] private DropItem itemPrefab;
        [SerializeField] private Transform spawnPoint;

        private void Start()
        {
            DI.Add(this);

            Spawn();
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var data = config.Items.GetRandomItem();

            var item = Instantiate(itemPrefab, spawnPoint);

            item.Init(data);
        }
    }
}
