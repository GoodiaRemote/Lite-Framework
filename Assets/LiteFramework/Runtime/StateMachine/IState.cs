namespace LiteFramework.Runtime.StateMachine
{
    public interface IState
    {
        public void StateEnter();
        public void StateFixedUpdate();
        public void StateUpdate();
        public void StateLateUpdate();
        public void StateExit();
    }
}