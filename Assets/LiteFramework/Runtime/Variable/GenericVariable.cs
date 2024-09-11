using System;
using LiteFramework.Runtime.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Runtime.Variable
{
    [DefaultExecutionOrder(-999)]
    public abstract class GenericVariable:DescriptionSO
    {
        
    }
    
    public abstract class GenericVariable<T> : GenericVariable
    {
        public T BaseValue;
        [SerializeField, ReadOnly] private T _value;
            
        public T Value
        {
            get =>_value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }

        public event Action<T> OnValueChange;
        

        private void OnEnable()
        {
            ResetToBaseValue();
        }
        
        public void ResetToBaseValue()
        {
            _value = BaseValue;
        }
        
#if UNITY_EDITOR
        [InlineButton(nameof(ApplyValue))]
        [NonSerialized, ShowInInspector] private T _newValue;
        private void ApplyValue()
        {
            Value = _newValue;
        }
#endif
    }
}