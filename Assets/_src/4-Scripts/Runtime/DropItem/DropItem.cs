using SGEngine.Configs.DropItem;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Rigidbody2D rigidbody2D;

        private DropItemData data;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Init(DropItemData data)
        {
            this.data = data;

            SetSprite(data.Sprite);
            SetScale(data.ScaleModificator);

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
        }

        public DropItem SetPosition(Vector3 pos)
        {
            transform.position = pos;

            return this;
        }

        private void SetScale(float scaleModificator)
        {
            transform.localScale *= scaleModificator;
        }

        private void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}