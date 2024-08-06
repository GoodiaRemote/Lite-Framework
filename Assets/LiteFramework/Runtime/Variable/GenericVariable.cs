using System;
using LiteFramework.Runtime.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.LiteFramework.Runtime.Variable
{
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
            _value = BaseValue;
        }
    }
}