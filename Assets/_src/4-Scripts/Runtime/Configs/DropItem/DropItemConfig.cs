using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [CreateAssetMenu(fileName = "DropItemConfig", menuName = "SGEngine/DropItemConfig", order = 0)]
    public class DropItemConfig : ScriptableObject
    {
        [SerializeField, TableList(ShowElementLabels = true)] 
        private List<DropItemData> items = new();

        public IReadOnlyList<DropItemData> Items => items;

        [ContextMenu("Generate IDs")]
        [Button(ButtonSizes.Large, "Generate IDs")]
        private void GenerateIds()
        {
            foreach (var itemData in items)
            {
                itemData.SetId(Guid.NewGuid().ToString());
            }

            SetNextMergeItemID();
        }

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
        [Button(ButtonSizes.Large, "Set Items Scale")]
        private void SetItemsScale(float minScale, float step)
        {
            var scale = minScale;

            foreach (var item in items)
            {
                item.SetScaleModificator(scale);

                scale += step;
            }
        }
    }
}