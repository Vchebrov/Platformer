using UnityEngine;

namespace FSM
{
    public class AttackState : State
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entering AttackState");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting AttackState");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Updating attack state");
        }
    }
}