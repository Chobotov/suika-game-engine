using System;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [Serializable]
    public class DropItemData
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private float scaleModificator;
        [SerializeField] private float weight;

        public Sprite Sprite => sprite;
        public float ScaleModificator => scaleModificator;
        public float Weight => weight;
    }
}