using UnityEngine;

namespace FSM
{
    public class RunState : State
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entering RunState");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting RunState");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Updating RunState");
        }
    }
}