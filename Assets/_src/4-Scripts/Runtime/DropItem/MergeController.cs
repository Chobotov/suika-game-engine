using Cysharp.Threading.Tasks;
using DG.Tweening;
using SGEngine.Configs.DropItem;
using SGEngine.Managers.Score;
using SGEngine.Runtime.App;
using UnityEngine;

namespace SGEngine.DropItem
{
    public class MergeController : MonoBehaviour
    {
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

                /*await UniTask.Delay(100);*/

                Merge(firstItem, secondItem, firstItemData);

                Debug.Log($"FirstItem : {firstItem.gameObject.name} is Equal SecondItem : {secondItem.gameObject.name}!");

                return true;
            }
            
            firstItem.CanCheckCollision = true;
            secondItem.CanCheckCollision = true;

            Debug.Log($"FirstItem : {firstItem.gameObject.name} is Not Equal SecondItem : {secondItem.gameObject.name}!");

            return false;
        }

        private void Merge(DropItem firstItem, DropItem secondItem, DropItemData firstItemData)
        {
            secondItem.SetRigidbodyType(RigidbodyType2D.Kinematic);
            secondItem.SetColliderState(false);

            firstItem.transform
                .DOMove(secondItem.transform.position, mergeDuration)
                .OnComplete(() =>
                {
                    firstItem.Destroy();
                    secondItem.Destroy();

                    spawnItems.SpawnItem(firstItemData.NextItemDataId, firstItem.transform.position);

                    ScoreManager.IncreaseScore(firstItemData.Score);
                });
        }
    }
}