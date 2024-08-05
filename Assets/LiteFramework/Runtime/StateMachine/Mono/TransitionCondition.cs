namespace LiteFramework.Runtime.StateMachine.Mono
{
    public abstract class TransitionCondition
    {
        private bool _expectedResult;

        public abstract void Init(MonoStateMachine runner);
        protected abstract bool CheckCondition();

        public void SetExpectedResult(bool expectedResult)
        {
            _expectedResult = expectedResult;
        }
        
        public bool IsMet()
        {
            var check = CheckCondition();
            var isMet = check == _expectedResult;
            return isMet;
        }
    }
}