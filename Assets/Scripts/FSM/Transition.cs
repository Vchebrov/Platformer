namespace FSM_for_test
{
    using System;

    public class Transition
    {
        public Type FromStateType { get; }
        public Type ToStateType { get; }
        public Func<bool> Condition { get; }

        public Transition(Type from, Type to, Func<bool> condition)
        {
            FromStateType = from;
            ToStateType = to;
            Condition = condition;
        }
    }

}