using System;
using LiteFramework.Runtime.Base;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using System.Collections.Generic;
#endif

namespace LiteFramework.Event
{
    public abstract class GenericEvent<T> : DescriptionSO
    {
        private Action<T> _event;
#if UNITY_EDITOR
        [ShowInInspector,HideInEditorMode, ReadOnly]
        private List<Delegate> _listeners = new();
#endif
        
        public void Register(Action<T> action)
        {
            _event += action;
#if UNITY_EDITOR
            _listeners.Add(action);
#endif
        }

        public void Unregister(Action<T> action)
        {
            _event -= action;
#if UNITY_EDITOR
            _listeners.Remove(action);
#endif
        }
        
        [Button]
        public void Raise(T value)
        {
            _event?.Invoke(value);
        }
    }
}