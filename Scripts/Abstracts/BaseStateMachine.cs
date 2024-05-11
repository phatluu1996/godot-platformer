using System;
using System.Collections.Generic;
using Godot;

public abstract class BaseStateMachine<EnumState> where EnumState : Enum
{
    protected Dictionary<EnumState, BaseState<EnumState>> States = new Dictionary<EnumState, BaseState<EnumState>>();
    
    public BaseState<EnumState> CurrentState;
    public BaseState<EnumState> LastState;
    public EnumState NextStateKey {get; private set;}  

    protected bool IsStateTransactioning;

    public virtual void Start(EnumState startStateKey){
        CurrentState = LastState = States[startStateKey];
        CurrentState.Enter();
    }

    public virtual void Excute(){
        EnumState nextStateKey = NextStateKey;

        if(!IsStateTransactioning && nextStateKey.Equals(CurrentState.StateKey)){
            CurrentState.Excute();
        }else if(!IsStateTransactioning){
            TransitionToState(nextStateKey);
        }        
    }

    public virtual void SetNextState(EnumState nextStateKey){
        NextStateKey = nextStateKey;        
    }

    public virtual void AddState(EnumState stateKey, BaseState<EnumState> state){
        state.StateKey = stateKey;
        States.Add(stateKey, state);
    }

    public virtual void TransitionToState(EnumState nextStateKey){
        IsStateTransactioning = true;
        LastState = CurrentState;
        CurrentState = States[nextStateKey];
        LastState.Exit();
        CurrentState.Enter();        
        IsStateTransactioning = false;
    }

    public virtual void DoStateTransitionBetween(EnumState from, EnumState to){
           
    }

    public virtual void DefaultTransition(EnumState from, EnumState to){

    }

    public virtual void OnFrameChangedEvent(int frame){
        CurrentState?.OnFrameChangedEvent(frame);
    }

    public virtual void OnAnimationFinished(string animationName){
        CurrentState?.OnAnimationFinished(animationName);
    }

    public virtual void OnAnimationLooped(string animationName){
        CurrentState?.OnAnimationLooped(animationName);
    }
}