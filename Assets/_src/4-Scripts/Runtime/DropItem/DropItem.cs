using System;
using SGEngine.Configs.DropItem;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class DropItem : MonoBehaviour, IEquatable<DropItem>
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool isDropped;

        private CircleCollider2D collider2D;
        private Rigidbody2D rigidbody2D;

        private DropItemData data;

        public DropItemData Data => data;

        public bool CanCheckCollision { get; set; }

        public bool IsDropped => isDropped;

        public bool IsDropProcess { get; set; }

        public event Action<DropItem, DropItem> OnCollisionDetect;
        public event Action<DropItem> OnDestroy;

        private void Awake()
        {
            collider2D = GetComponent<CircleCollider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Init(DropItemData data)
        {
            this.data = data;

            SetSprite(data.Sprite);
            SetScale(data.ScaleModificator);

            CanCheckCollision = true;

            Activate();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);

            SetRigidbodyType(RigidbodyType2D.Kinematic);
        }

        public void SetRigidbodyType(RigidbodyType2D bodyType)
        {
            rigidbody2D.bodyType = bodyType;

            isDropped = rigidbody2D.bodyType == RigidbodyType2D.Dynamic;
        }

        public void SetColliderState(bool state)
        {
            collider2D.enabled = state;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);

            Destroy(gameObject);
        }

        private void SetScale(float scaleModificator)
        {
            transform.localScale *= scaleModificator;
        }

        private void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!isDropped || !CanCheckCollision) return;

            if (col.gameObject.TryGetComponent<DropItem>(out var item))
            {
                if (item.isDropped && item.CanCheckCollision)
                {
                    OnCollisionDetect?.Invoke(this, item);
                }
            }
        }

        #region Equals

        public bool Equals(DropItem other)
        {
            return Equals(data, other.data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DropItem)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), data);
        }

        #endregion
    }
}