namespace LiteFramework.Runtime.StateMachine.Mono
{
    public interface IAction
    {
        public void ActionInit(MonoStateMachine runner);
        public void ActionEnter();
        public void ActionFixedUpdate();
        public void ActionUpdate();
        public void ActionLateUpdate();
        public void ActionExit();
    }
}