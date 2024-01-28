using System;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [Serializable]
    public class DropItemData : IEquatable<DropItemData>
    {
        [SerializeField] private string name;
        [SerializeField] private string id; 
        //[Title("View")]
        [SerializeField] private Sprite sprite;
        [SerializeField, Range(0, 10)] private float scaleModificator;
        //[Title("Settings")]
        [SerializeField, Range(0, 1)] private float weight;
        [SerializeField] private int score;
        //[Title("Merge item Id")]
        [SerializeField] private string nextItemDataId;

        public string ID => id;
        public int Score => score;
        public string NextItemDataId => nextItemDataId;

        public bool HasNextItem => !string.IsNullOrEmpty(nextItemDataId);

        public Sprite Sprite => sprite;
        public float ScaleModificator => scaleModificator;
        public float Weight => weight;

        internal void SetId(string id)
        {
            this.id = id;
        }

        internal void SetNextItemId(string id)
        {
            nextItemDataId = id;
        }

        internal void SetScaleModificator(float scaleModificator)
        {
            this.scaleModificator = scaleModificator;
        }

        #region Equals

        public bool Equals(DropItemData other)
        {
            return other.id.Equals(id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DropItemData)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}