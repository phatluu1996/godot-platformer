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

    public override void DoStateTransitionBetween(EPlayerState from, EPlayerState to)
    {
        Debug.Log("[STATE] " + from + " -> " + to);

        // if (!Player.IsAttacking)
        // {
        //     DefaultTransition(to);
        // }
        // else
        // {
        //     Player.WeaponController.Main.AttackTransition(States[from] as PlayerState, States[to] as PlayerState);
        // }
        Player.AC.TransitAnimation(FindState(from), FindState(to));
    }

    public virtual PlayerState FindState(EPlayerState ePlayerState)
    {
        return States[ePlayerState] as PlayerState;
    }
}