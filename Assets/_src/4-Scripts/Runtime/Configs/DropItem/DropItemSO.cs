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
            propertyInfo.SetValue(window, true, null);

            Selection.activeObject = oldSelection;
        }
    }
}