using System;
using System.Collections.Generic;
using Godot;

public class PlayerStateMachine : BaseStateMachine<EPlayerState>
{
    public Player Player;
    protected Dictionary<(EPlayerState, EPlayerState, EPlayerWeapon), PlayerStateTransitionCallback> StateTransitions = new Dictionary<(EPlayerState, EPlayerState, EPlayerWeapon), PlayerStateTransitionCallback>();

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }

    public override void DoStateTransitionBetween(EPlayerState from, EPlayerState to){
        GD.Print(from + " -> " + to);
        base.DoStateTransitionBetween(from, to);  
        if(!StateTransitions.ContainsKey((from, to, Player.WeaponController.Main.WeaponType)) || !Player.IsAttacking){
            //Default transition: cancel current and play new
            DefaultTransition(from, to);
            GD.Print("Use default transition");
        }else{
            StateTransitions[(from, to, Player.WeaponController.Main.WeaponType)]?.Execute();  
        }     
    }

    public void AddStateTransition(PlayerStateTransitionCallback transitionCallback){
        (EPlayerState, EPlayerState, EPlayerWeapon) key = transitionCallback.TransitionKey;
        if(!StateTransitions.ContainsKey(key)){            
            StateTransitions.Add(key, transitionCallback);
        }
    }

    public virtual PlayerState FindState(EPlayerState ePlayerState){
        return States[ePlayerState] as PlayerState;
    }

    public override void DefaultTransition(EPlayerState from, EPlayerState to)
    {
        base.DefaultTransition(from, to);
        Player.WeaponController.Main.Reset();
        PlayerState state = States[to] as PlayerState;
        PlayerAnimation animation = state.Animation[EPlayerWeapon.NONE].normal[state.TransitedAnimationIndex()];
        Player.PlayAnimation(animation.name, animation.transitedFrame, 0);
    }
}