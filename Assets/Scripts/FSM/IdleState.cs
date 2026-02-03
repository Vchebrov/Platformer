using UnityEngine;

namespace FSM
{
    public class IdleState : State
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entering IdleState");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting IdleState");
        }
    }
}