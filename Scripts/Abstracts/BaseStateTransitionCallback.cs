using System;


public class BaseStateTransitionCallback<EnumState> where EnumState : Enum
{
    public (EnumState, EnumState) TransitionKey;
    public BaseStateMachine<EnumState> FSM;
    protected BaseState<EnumState> FromState;
    protected BaseState<EnumState> ToState;
    protected Action<BaseState<EnumState>, BaseState<EnumState>> DoTransition;

    public BaseStateTransitionCallback(BaseStateMachine<EnumState> fsm, EnumState from, EnumState to, Action<BaseState<EnumState>, BaseState<EnumState>> callback)
    {
        FSM = fsm;
        TransitionKey = (from, to);
        FromState = FSM.FindState(from);
        ToState = FSM.FindState(to);
        DoTransition = callback;
    }

    public virtual void Execute(){
        DoTransition?.Invoke(FromState, ToState);
    }
}
