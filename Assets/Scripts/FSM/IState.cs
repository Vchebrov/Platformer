namespace FSM_for_test
{
    public interface IState
    {
        public void Enter();
        
        public void Exit();
        
        public void Update();
    }
}