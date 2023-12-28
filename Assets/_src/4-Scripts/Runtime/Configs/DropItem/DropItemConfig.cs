using System.Collections.Generic;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [CreateAssetMenu(fileName = "DropItemConfig", menuName = "SGEngine/DropItemConfig", order = 0)]
    public class DropItemConfig : ScriptableObject
    {
        [SerializeField] private List<DropItemData> items = new();

        public IReadOnlyList<DropItemData> Items => items;
    }
}