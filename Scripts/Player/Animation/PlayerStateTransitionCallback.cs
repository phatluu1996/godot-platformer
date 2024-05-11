using System;


public class PlayerStateTransitionCallback
{
    public (EPlayerState, EPlayerState, EPlayerWeapon) TransitionKey;
    public PlayerStateMachine FSM;
    protected PlayerState FromState;
    protected PlayerState ToState;
    protected Action<PlayerState, PlayerState> DoTransition;

    public PlayerStateTransitionCallback(PlayerStateMachine fsm, EPlayerState from, EPlayerState to, EPlayerWeapon weaponType, Action<PlayerState, PlayerState> callback)
    {
        FSM = fsm;
        TransitionKey = (from, to, weaponType);
        FromState = FSM.FindState(from);
        ToState = FSM.FindState(to);
        DoTransition = callback;
    }

    public void Execute(){
        DoTransition?.Invoke(FromState, ToState);
    }
}

