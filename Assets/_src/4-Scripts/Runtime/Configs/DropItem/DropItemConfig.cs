using System;
using System.Collections.Generic;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [CreateAssetMenu(fileName = "DropItemConfig", menuName = "SGEngine/DropItemConfig", order = 0)]
    public class DropItemConfig : ScriptableObject
    {
        [SerializeField] private List<DropItemData> items = new();
        [Space]
        [SerializeField, Range(0, 100)] private float minScale;
        [SerializeField, Range(0, 100)] private float stepScale;

        public IReadOnlyList<DropItemData> Items => items;

        [ContextMenu("Generate IDs")]
        private void GenerateIds()
        {
            foreach (var itemData in items)
            {
                itemData.SetId(Guid.NewGuid().ToString());
            }
        }
        
        [ContextMenu("Set Next Merge Item ID")]
        private void SetNextMergeItemID()
        {
            for (var i = 0; i < items.Count; i++)
            {
                var nextItemIndex = i + 1;

                if (nextItemIndex < items.Count)
                {
                    items[i].SetNextItemId(items[nextItemIndex].ID);
                }
            }
        }
        
        [ContextMenu("Set Items Scale")]
        private void SetItemsScale()
        {
            var scale = minScale;

            foreach (var item in items)
            {
                item.SetScaleModificator(scale);

                scale += stepScale;
            }
        }
    }
}