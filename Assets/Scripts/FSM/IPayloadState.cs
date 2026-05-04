namespace FSM_for_test
{
    public interface IPayloadState<TPayLoad>: IState
    {
       void Enter(TPayLoad payLoad); 
    }
}