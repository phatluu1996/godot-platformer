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

    public override void DoStateTransitionBetween(EPlayerState from, EPlayerState to)
    {
        Debug.Log(from + " -> " + to);
        base.DoStateTransitionBetween(from, to);
        // if (!StateTransitions.ContainsKey((from, to, Player.WeaponController.Main.WeaponType)) || !Player.IsAttacking)
        // {
        //     //Default transition: cancel current and play new
        //     DefaultTransition(from, to);
        //     Debug.Log("Use default transition");
        // }
        // else
        // {
        //     Player.WeaponController.Main.AttackTransition(from, to);
        //     StateTransitions[(from, to, Player.WeaponController.Main.WeaponType)]?.Execute();
        // }
        if(!Player.IsAttacking){
            DefaultTransition(to);
        }else{
            Player.WeaponController.Main.AttackTransition(States[from] as PlayerState, States[to] as PlayerState);
        }
    }

    public void AddStateTransition(PlayerStateTransitionCallback transitionCallback)
    {
        (EPlayerState, EPlayerState, EPlayerWeapon) key = transitionCallback.TransitionKey;
        if (!StateTransitions.ContainsKey(key))
        {
            StateTransitions.Add(key, transitionCallback);
        }
    }

    public virtual PlayerState FindState(EPlayerState ePlayerState)
    {
        return States[ePlayerState] as PlayerState;
    }

    public override void DefaultTransition(EPlayerState stateKey)
    {
        base.DefaultTransition(stateKey);
        Player.WeaponController.Main.Reset();
        PlayerState state = States[stateKey] as PlayerState;
        PlayerAnimation animation = state.Animation[EPlayerWeapon.NONE].normal[state.TransitedAnimationIndex()];
        Player.AC.PlayAnimation(animation, 0, 0);
    }


}