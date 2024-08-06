using LiteFramework.Runtime.Variable.Type;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private IntVariable _intVariable;
    [SerializeField] private StringVariable _stringVariable;
    [SerializeField] private ColorVariable _colorVariable;
    [SerializeField] private Vector2Variable _vector2Variable;
    [SerializeField] private Vector3Variable _vector3Variable;
    [SerializeField] private GameObjectVariable _gameObjectVariable;

    [Button]
    private void TestValue()
    {
        _intVariable.Value++;
    }
}
