using System.Collections.Generic;
using System.Linq;
using LiteFramework.Runtime.Utils;
using UnityEngine;

namespace LiteFramework.Runtime.StateMachine
{
    public class StateBlock
    {
        public int Hash;
        public IState State;
        public string Name;
        public List<Transition> Transitions;
        public float ExitTime;
    }
    
    public class StateMachine
    {
        private enum Event
        {
            Enter,
            Update,
            Exit
        }
        
        private readonly Dictionary<int, StateBlock> _stateBlocks = new();
        private Event _event;
        private bool _initialized;
        
        public string[] AllStatesName => _stateBlocks.Select(s => s.Value.Name).ToArray();
        public float StateRunTime { get; private set; }
        public StateBlock StartBlockState { get; private set; }
        public StateBlock CurrentBlockState { get; private set; }


        public void FixedUpdate()
        {
            if(!_initialized) return;
            if(_event != Event.Update) return;
            CurrentBlockState.State.StateFixedUpdate();
        }
        
        public void Update()
        {
            if(!_initialized) return;
            if(_event != Event.Update) return;
            CurrentBlockState.State.StateUpdate();
            StateRunTime += Time.deltaTime;
            CheckCondition();
        }
        
        public void LateUpdate()
        {
            if(!_initialized) return;
            if(_event != Event.Update) return;
            CurrentBlockState.State.StateLateUpdate();
        }

        public string GetStateName(int nameHash)
        {
            if (_stateBlocks.TryGetValue(nameHash, out var stateBlock))
            {
                return stateBlock.Name;
            }

            return default;
        }

        public void Init()
        {
            _initialized = true;
            if (StartBlockState is not null)
            {
                ChangeState(StartBlockState.Hash);
            }
        }
        
        public void SetStartState(int nameHash)
        {
            if (_stateBlocks.TryGetValue(nameHash, out var stateBlock))
            {
                StartBlockState = stateBlock;
                return;
            }

            Debug.LogWarning($"State {nameHash.ToString().ColorLog(Color.yellow)} not found in state machine");
        }
        
        public void SetStartState(string name)
        {
            var hash = Animator.StringToHash(name);
            if (_stateBlocks.TryGetValue(hash, out var stateBlock))
            {
                StartBlockState = stateBlock;
                return;
            }

            Debug.LogWarning($"State {name.ColorLog(Color.yellow)} not found in state machine");
        }

        public void AddState(string name, IState state = null, float exitTime = 1)
        {
            var newState = state ?? new EmptyState();
            var hash = Animator.StringToHash(name);
            var block = new StateBlock
            {
                Hash = hash,
                State = newState,
                ExitTime = exitTime,
                Name = name,
                Transitions = new List<Transition>()

            };
            if (!_stateBlocks.TryAdd(hash, block))
            {
                Debug.LogWarning($"State {name.ColorLog(Color.yellow)} already add in state machine");
            }

        }
        
        public void RemoveState(int nameHash)
        {
            if(_stateBlocks.Remove(nameHash))
            {
                return;
            }
            Debug.LogWarning($"State {nameHash.ToString().ColorLog(Color.yellow)} not found in state machine");
        }

        public void RemoveState(string name)
        {
            var hash = Animator.StringToHash(name);
            if (_stateBlocks.Remove(hash))
            {
                return;
            }
            Debug.LogWarning($"State {name.ColorLog(Color.yellow)} not found in state machine");
        }

        private void CheckCondition()
        {
            for (int i = 0; i < CurrentBlockState.Transitions.Count; i++)
            {
                var transition = CurrentBlockState.Transitions[i];
                if(!transition.CheckConditions()) continue;
                if (transition.HasExitTime)
                {
                    if (!(StateRunTime >= CurrentBlockState.ExitTime)) continue;
                    ChangeState(transition.ToStateHash);
                    return;
                }
                ChangeState(transition.ToStateHash);
                return;
            }
        }

        public void AddTransition(Transition transition)
        {
            if(!ValidateTransition(transition))
            {
                return;
            }
            if (_stateBlocks.TryGetValue(transition.FromStateHash, out var stateBlock))
            {
                stateBlock.Transitions.Add(transition);
            }

        }

        public void AddTwoWayTransition(Transition transition)
        {
            if(!ValidateTransition(transition))
            {
                return;
            }
            //Add first transition
            AddTransition(transition);
            
            //create reverse transition
            bool ReverseCondition() => transition.Condition == null || !transition.Condition();
            var reverseTransition = new Transition(transition.To, transition.From, ReverseCondition, transition.HasExitTime);
            //Add reverse transition
            AddTransition(reverseTransition);
        }

        private bool ValidateTransition(Transition transition)
        {
            if(!_stateBlocks.ContainsKey(transition.FromStateHash))
            {
                Debug.LogWarning($"State {transition.From.ColorLog(Color.yellow)} not found in state machine");
                return false;
            }
            if(!_stateBlocks.ContainsKey(transition.ToStateHash))
            {
                Debug.LogWarning($"State {transition.To.ColorLog(Color.yellow)} not found in state machine");
                return false;
            }

            return true;
        }
        
        public void ChangeState(int nameHash)
        {
            if (CurrentBlockState is not null && CurrentBlockState.Hash == nameHash) return;
            if (_stateBlocks.TryGetValue(nameHash, out var nextStateBlock))
            {
                if (CurrentBlockState is not null)
                {
                    _event = Event.Exit;
                    CurrentBlockState.State.StateExit();
                }
                _event = Event.Enter;
                CurrentBlockState = nextStateBlock;
                CurrentBlockState.State.StateEnter();
                StateRunTime = 0;
                _event = Event.Update;
            }
        }

        public void ChangeState(string name)
        {
            var stateHash = Animator.StringToHash(name);
            ChangeState(stateHash);
        }
    }
}