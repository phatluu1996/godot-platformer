

using System.Collections.Generic;
using Godot;

public class PlayerClimbDownState : PlayerState
{
    public PlayerClimbDownState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity = Vector2.Zero;
        Player.x = Player.Ladder.Position.X;
    }

    protected override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
        switch (frame)
        {
            case 1:
                Player.y += 21;
                break;

            case 3:
                Player.y += 19;
                break;
        }
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
        FSM.SetNextState(EPlayerState.CLIMB);
    }
    public override void OnAnimationLooped(string animationName)
    {
        base.OnAnimationLooped(animationName);
    }
}