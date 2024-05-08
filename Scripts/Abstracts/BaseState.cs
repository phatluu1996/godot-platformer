
using System;

public abstract class BaseState<EnumState> where EnumState : Enum
{
    public EnumState StateKey {get;set;}    
    public abstract void Enter();
    public abstract void Excute();
    protected abstract void PreUpdate();
    protected abstract void Update();
    protected abstract void PostUpdate();
    public abstract void Exit();
    public abstract void OnFrameChangedEvent(int frame);
    public abstract void OnAnimationFinished(string animationName);
    public abstract void OnAnimationLooped(string animationName);
    public abstract string TransitedAnimation();
}