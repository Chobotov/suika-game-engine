using System;
using System.Reflection;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace SGEngine.Configs.DropItem
{
    [Serializable]
    public class DropItemSO
    {
        [field: SerializeField] public DropItemDataSO ItemSO { get; private set; }

        public DropItemSO(DropItemDataSO so)
        {
            ItemSO = so;
        }
        
        [Button("Показать")]
        private void Show()
        {
            var oldSelection = Selection.activeObject;
            Selection.activeObject = ItemSO;

            var inspectorWindowType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");

            MethodInfo method = typeof(EditorWindow).GetMethod(nameof(EditorWindow.CreateWindow), new[] { typeof(System.Type[]) });
            MethodInfo generic = method.MakeGenericMethod(inspectorWindowType);

            var window = generic.Invoke(null, new object[] { new System.Type[] { } });

            PropertyInfo propertyInfo = inspectorWindowType.GetProperty("isLocked");
            bool value = (bool)propertyInfo.GetValue(window, null);
            propertyInfo.SetValue(window, true, null);

            Selection.activeObject = oldSelection;
        }
    }
    
    public class DropItemDataSO : ScriptableObject, IEquatable<DropItemDataSO>
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

        public bool Equals(DropItemDataSO other)
        {
            return other.id.Equals(id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DropItemDataSO)obj);
        }

        #endregion
    }
}