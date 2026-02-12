using UnityEngine;

namespace FSM
{
    public class WalkState : State
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entering WalkState");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting WalkState");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Updating WalkState");
        }
    }
}