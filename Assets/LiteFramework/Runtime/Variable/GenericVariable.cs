using System;
using LiteFramework.Runtime.Base;

namespace LiteFramework.LiteFramework.Runtime.Variable
{
    public abstract class GenericVariable<T> : DescriptionSO
    {
        public T BaseValue;
        private T _value;
            
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