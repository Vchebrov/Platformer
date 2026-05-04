namespace FSM_for_test
{
    using System;

    public class Transition
    {
        public Type FromStateType { get; }
        public Type ToStateType { get; }
        public Func<bool> Condition { get; }
        public Func<object> PayloadProvider { get; }

        public Transition(Type from, Type to, Func<bool> condition, Func<object> payLoadProvider = null)
        {
            FromStateType = from;
            ToStateType = to;
            Condition = condition;
            PayloadProvider = payLoadProvider;
        }
    }

}