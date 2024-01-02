using Cysharp.Threading.Tasks;
using DG.Tweening;
using SGEngine.Managers.Score;
using SGEngine.Runtime.App;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class MergeController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip mergeSound;
        [Space]
        [SerializeField] private SpawnItems spawnItems;
        [SerializeField] private float mergeDuration = 0.15f;

        private IScoreManager scoreManager;

        private IScoreManager ScoreManager => scoreManager ??= DI.Get<IScoreManager>();

        public async UniTask<bool> TryMerge(DropItem firstItem, DropItem secondItem)
        {
            firstItem.CanCheckCollision = false;
            secondItem.CanCheckCollision = false;

            if (firstItem.Equals(secondItem))
            {
                var firstItemData = firstItem.Data;

                if (!firstItemData.HasNextItem) return false;

                Merge(firstItem, secondItem);

                Debug.Log($"FirstItem : {firstItem.gameObject.name} is Equal SecondItem : {secondItem.gameObject.name}!");

                return true;
            }

            firstItem.CanCheckCollision = true;
            secondItem.CanCheckCollision = true;

            Debug.Log($"FirstItem : {firstItem.gameObject.name} is Not Equal SecondItem : {secondItem.gameObject.name}!");

            return false;
        }

        private void Merge(DropItem firstItem, DropItem secondItem)
        {
            var data = firstItem.Data;

            secondItem.SetRigidbodyType(RigidbodyType2D.Kinematic);
            secondItem.SetColliderState(false);

            firstItem.transform
                .DOMove(secondItem.transform.position, mergeDuration)
                .OnComplete(() =>
                {
                    firstItem.Destroy();
                    secondItem.Destroy();

                    audioSource.PlayOneShot(mergeSound);

                    particleSystem.transform.SetPositionAndRotation(firstItem.transform.position, Quaternion.identity);
                    particleSystem.Play();

                    spawnItems.SpawnItem(data.NextItemDataId, firstItem.transform.position);

                    ScoreManager.IncreaseScore(data.Score);
                });
        }
    }
}