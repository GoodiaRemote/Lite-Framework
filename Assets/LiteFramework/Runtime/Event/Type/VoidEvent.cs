using System;
using LiteFramework.Runtime.Base;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using System.Collections.Generic;
#endif

namespace LiteFramework.Runtime.Event.Type
{
    [CreateAssetMenu(menuName = "LiteFramework/Event/Void")]
    public class VoidEvent : DescriptionSO
    {
        private Action _event;
#if UNITY_EDITOR
        [ShowInInspector, HideInEditorMode, ReadOnly]
        private List<Delegate> _listeners = new();
#endif
        
        public void Register(Action action)
        {
            _event += action;
#if UNITY_EDITOR
            _listeners.Add(action);
#endif
        }

        public void Unregister(Action action)
        {
            _event -= action;
#if UNITY_EDITOR
            _listeners.Remove(action);
#endif
        }
        
        [Button]
        public void Raise()
        {
            _event?.Invoke();
        }
    }
}