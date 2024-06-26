using System.Collections.Generic;
using Godot;

public class PlayerClimbState : PlayerState
{
    int currentFrame;
    float currentFrameProgress;
    public PlayerClimbState(global::Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity = Vector2.Up;
        Player.x = Player.Ladder.Position.X;
        Player.OnMomentum = false;
    }

    protected override void Update()
    {

        base.Update();
        // Input.Listen();
        if (Input.xHAxis != 0)
        {
            Player.Facing = Input.xHAxis;
        }
        Player.velocity.Y = Input.yHAxis * Constants.CLIMB_SPEED * (!Player.IsAttacking).GetHashCode();
        if (Player.IsAttacking)
        {
            Player.AS.SpeedScale = 1f;
        }
        else
        {
            Player.AS.SpeedScale = -Mathf.Sign(Player.velocity.Y);
            if (Input.yPAxis != 0)
            {
                Player.AC.PlayAnimation(Animation[EPlayerWeapon.NONE].normal[0], currentFrame, currentFrameProgress);
            }
        }

        if (!Input.Up.Held && !Input.Down.Held)
        {
            currentFrame = Player.AS.Frame;
            currentFrameProgress = Player.AS.FrameProgress;
        }
        if (Player.y < Player.Ladder?.Position.Y + 38)
        {
            FSM.SetNextState(EPlayerState.CLIMBUP);
        }
        else if (Input.Jump.Pressed)
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Player.IsOnFloor())
        {
            FSM.SetNextState(Input.xHAxis != 0 ? EPlayerState.WALK : EPlayerState.IDLE);
        }
        else if (!Player.CanClimbLadder)
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Player.AS.SpeedScale = 1;
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
    }
    public override void OnAnimationLooped(string animationName)
    {
        base.OnAnimationLooped(animationName);
    }
}