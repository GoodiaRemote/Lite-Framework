using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Runtime.StateMachine.Mono.SO
{
    [CreateAssetMenu(fileName = "New State Controller", menuName = "LiteFramework/State Machine/State Controller", order = 0)]
    public class StateControllerSO : ScriptableObject
    {
        [SerializeField, ValueDropdown(nameof(AllStates))] private string _startState = "None";
        [SerializeField] private TransitionItem[] _transitions;
        
        private List<string> AllStates
        {
            get
            {
                var states = new List<string> { "None" };
                if (_transitions == null) return states;
                foreach (var transitionItem in _transitions)
                {
                    if (!states.Contains(transitionItem.From.StateName))
                    {
                        states.Add(transitionItem.From.StateName);
                    }

                    if (!states.Contains(transitionItem.To.StateName))
                    {
                        states.Add(transitionItem.To.StateName);
                    }
                }

                return states;
            }
        }

        public StateMachine CreateStateMachine(MonoStateMachine runner)
        {
            var stateMachine = new StateMachine();
            
            var states = new Dictionary<string, IState>();
            foreach (var transitionItem in _transitions)
            {
                IState state;
                //try add FromState
                if (!states.TryGetValue(transitionItem.From.StateName, out state))
                {
                    state = transitionItem.From.CreateState(runner);
                    stateMachine.AddState(transitionItem.From.StateName, state, transitionItem.From.ExitTime);
                    states.Add(transitionItem.From.StateName, state);
                }
                
                //try add ToState
                if (!states.TryGetValue(transitionItem.To.StateName, out state))
                {
                    state = transitionItem.To.CreateState(runner);
                    stateMachine.AddState(transitionItem.To.StateName, state, transitionItem.To.ExitTime);
                    states.Add(transitionItem.To.StateName, state);
                }
                
                var transition = new Transition
                {
                    From = transitionItem.From.StateName,
                    To = transitionItem.To.StateName,
                    HasExitTime = transitionItem.HasExitTime,
                    Condition = GetConditionUsage(runner, transitionItem.Conditions)
                };
                if (transitionItem.TwoWayTransition)
                {
                    stateMachine.AddTwoWayTransition(transition);
                }
                else
                {
                    stateMachine.AddTransition(transition);
                }
            }

            if (!string.IsNullOrEmpty(_startState) && _startState != "None")
            {
                stateMachine.SetStartState(_startState);
            }
            return stateMachine;
        }

        private static Func<bool> GetConditionUsage(MonoStateMachine stateMachine, ConditionUsage[] conditionUsages)
        {
            List<TransitionCondition> conditions =new();
            for (int i = 0; i < conditionUsages.Length; i++)
            {
                var condition = conditionUsages[i].Condition.GetCondition(conditionUsages[i].ExpectedResult == Result.True);
                condition.Init(stateMachine);
                conditions.Add(condition);
            }

            bool ConditionFunc()
            {
                var count = conditionUsages.Length;
                for (int i = 0; i < count; i++)
                {
                    if (conditions[i].IsMet()) continue;
                    if (conditionUsages[i].Operator == Operator.Or && i < count - 1) continue;
                    return false;
                }

                return true;
            }

            return ConditionFunc;
        }
    }

    [Serializable]
    public struct TransitionItem
    {
        private const string RIGHT_ARROW = "\u2192";
        private const string TWO_WAY_ARROW = "\u2194";
        
        [Required]
        [FoldoutGroup("@"+nameof(TransitionTitle))]
        [InlineEditor] public StateSO From;
        [Required]
        [FoldoutGroup("@"+nameof(TransitionTitle))]
        [InlineEditor] public StateSO To;
        [FoldoutGroup("@"+nameof(TransitionTitle))]
        public bool TwoWayTransition;
        [FoldoutGroup("@"+nameof(TransitionTitle))]
        public bool HasExitTime;
        [FoldoutGroup("@"+nameof(TransitionTitle))]
        public ConditionUsage[] Conditions;
        
        private string TransitionTitle  {
            get
            {
                if (From == null && To == null) return "New Transition";
                var title = $"[From] {(TwoWayTransition ? TWO_WAY_ARROW : RIGHT_ARROW)} [To]";
                title = title.Replace("[From]", From != null ? From.StateName : "");
                title = title.Replace("[To]", To != null ? To.StateName : "");
                return title;
            }
        }
        
    }
    
    [Serializable]
    public struct ConditionUsage
    {
        [Required]
        [FoldoutGroup("@"+nameof(ConditionTitle))]
        [InlineEditor] public ConditionSO Condition;
        [FoldoutGroup("@"+nameof(ConditionTitle))]
        public Result ExpectedResult;
        [FoldoutGroup("@"+nameof(ConditionTitle))]
        public Operator Operator;
        
        private string ConditionTitle => Condition == null ? "New Condition" : $"{Condition.name} is {ExpectedResult.ToString().ToUpper()} - {Operator.ToString().ToUpper()}";
    }
    
    public enum Result
    {
        True,
        False
    }

    public enum Operator
    {
        Or,
        And
    }
}