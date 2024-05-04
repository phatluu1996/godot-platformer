using System;
using Godot;

public class PlayerStateMachine : BaseStateMachine<EPlayerState>
{
    public Player Player;

    public PlayerStateMachine(Player player)
    {
        Player = player;
    }
}