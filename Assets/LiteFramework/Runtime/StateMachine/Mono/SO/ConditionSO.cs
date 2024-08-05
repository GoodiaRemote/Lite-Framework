using LiteFramework.Runtime.Base;

namespace LiteFramework.Runtime.StateMachine.Mono.SO
{
    public abstract class ConditionSO : DescriptionSO
    {
        protected abstract TransitionCondition CreateCondition();

        public TransitionCondition GetCondition(bool expectedResult)
        {
            var condition = CreateCondition();
            condition.SetExpectedResult(expectedResult);
            return condition;
        }
    }

    public class ConditionSO<T> : ConditionSO where T : TransitionCondition, new()
    {
        protected override TransitionCondition CreateCondition()
        {
            return new T();
        }
    }
}