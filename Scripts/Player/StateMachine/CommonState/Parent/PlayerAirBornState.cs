using System.Collections.Generic;
using Godot;

public class PlayerAirBornState : PlayerState
{
    public PlayerAirBornState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    protected override void Update()
    {
        base.Update();
        // Input.Listen();
        if (Input.xHAxis != 0)
        {
            Player.Facing = Input.xHAxis;
        }

        Player.GravityForceApply();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
    }
}