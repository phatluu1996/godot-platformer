

using System.Collections.Generic;
using Godot;

public class PlayerClimbUpState : PlayerState
{
    public PlayerClimbUpState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity = Vector2.Zero;
        Player.x = Player.Ladder.Position.X;
        Player.y = Player.Ladder.Position.Y + 38f;
    }

    public override void Update()
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
            case 2:
                Player.y -= 17;
                break;

            case 4:
                Player.y -= 21.05f;
                break;
        }
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
        FSM.SetNextState(EPlayerState.IDLE);
    }
    public override void OnAnimationLooped(string animationName)
    {
        base.OnAnimationLooped(animationName);
    }
}