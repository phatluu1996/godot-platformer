using System;


public class PlayerStateTransitionCallback : BaseStateTransitionCallback<EPlayerState>
{
    public Player Player;
    public PlayerStateTransitionCallback(Player player, EPlayerState from, EPlayerState to, Action<BaseState<EPlayerState>, BaseState<EPlayerState>> callback) : base(player.FSM, from, to, callback)
    {
        Player = player;
    }

    public override void Execute()
    {
        base.Execute();
    }
}
