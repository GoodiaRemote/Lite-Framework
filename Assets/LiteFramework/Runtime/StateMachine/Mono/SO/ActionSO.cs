using LiteFramework.Runtime.Base;

namespace LiteFramework.Runtime.StateMachine.Mono.SO
{
    public abstract class ActionSO : DescriptionSO
    {
        public abstract IAction CreateAction();
    }

    public class ActionSO<T> : ActionSO where T : IAction, new()
    {
        public override IAction CreateAction()
        {
            return new T();
        }
    }
}