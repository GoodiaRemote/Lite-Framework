using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    [CreateAssetMenu(menuName = "LiteFramework/DescriptionSO", fileName = "NewDescription")]
    public class DescriptionSO : SerializedScriptableObject
    {
        [BoxGroup("Description")]
        [TextArea, HideLabel] public string Description;
    }
}