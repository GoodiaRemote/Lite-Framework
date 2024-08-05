using System;
using UnityEngine;

namespace LiteFramework.Runtime.StateMachine
{
    public class Transition
    {
        private string _from;
        private string _to;
            
            
        public Func<bool> Condition;
        public string From
        {
            get => _from;
            set
            {
                _from = value;
                FromStateHash = Animator.StringToHash(_from);
            }
        }

        public string To
        {
            get => _to;
            set
            {
                _to = value;
                ToStateHash = Animator.StringToHash(_to);
            }
        }
        public bool HasExitTime;
        public int FromStateHash { get; private set; }
        public int ToStateHash { get; private set; }
        
        
        public Transition()
        {
            
        }
        
        public Transition(string from, string to)
        {
            From = from;
            To = to;
            Condition = null;
            HasExitTime = true;
        }

        public Transition(string from, string to, Func<bool> condition, bool hasExitTime = true)
        {
            From = from;
            To = to;
            Condition = condition;
            HasExitTime = hasExitTime;
        }

        public bool CheckConditions()
        {
            return Condition == null || Condition();
        }
    }
}