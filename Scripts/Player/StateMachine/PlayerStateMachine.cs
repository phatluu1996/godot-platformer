using System;
using System.Collections.Generic;
using Godot;

public class PlayerStateMachine : BaseStateMachine<EPlayerState>
{
    public Player Player;

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }

    public override void DoStateTransitionBetween(EPlayerState from, EPlayerState to){
        base.DoStateTransitionBetween(from, to);  
    }

    public override void DefaultTransition(EPlayerState from, EPlayerState to)
    {
        base.DefaultTransition(from, to);
        Player.WeaponController.Main.Reset();
        PlayerState state = States[to] as PlayerState;
        PlayerAnimation animation = state.Animation[EPlayerWeapon.NONE].normal[0];
        Player.PlayAnimation(animation.name, animation.transitedFrame, 0);
    }
}