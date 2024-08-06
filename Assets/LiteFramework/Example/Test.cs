using LiteFramework.LiteFramework.Runtime.Variable.Type;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private IntVariable _intVariable;
    [SerializeField] private StringVariable _stringVariable;
    [SerializeField] private ColorVariable _colorVariable;
    [SerializeField] private Vector2Variable _vector2Variable;

    [Button]
    private void TestValue()
    {
        _intVariable.Value++;
    }
}
