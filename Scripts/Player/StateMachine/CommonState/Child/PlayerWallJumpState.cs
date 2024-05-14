using System.Collections.Generic;
using Godot;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity.Y = -Constants.JUMP_FORCE;
        if (Player.OnMomentum)
        {
            Player.FlipH();
        }
    }

    protected override void Update()
    {
        base.Update();
        Timer += Player.GetProcessDeltaTime();
        // Input.Listen();

        float movingSpeed = Player.OnMomentum ? Constants.MOMENTUM_SPEED : Constants.WALK_SPEED;
        Player.GravityForceApply();
        if (Timer <= 0.18f)
        {
            Player.velocity.X = -Player.Facing * movingSpeed;
            if (Player.OnMomentum)
            {
                Player.velocity.X = Player.Facing * movingSpeed;
            }
        }
        else
        {
            if (Input.xHAxis != 0)
            {
                Player.Facing = Input.xHAxis;
            }
            Player.velocity.X = Input.xHAxis * movingSpeed;
        }

        if (Player.velocity.Y >= 0f || !Input.Jump.Held || Player.IsOnCeilingOnly())
        {
            Player.velocity.Y = 0;
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Player.CanClimbLadder && Input.Up.Pressed)
        {
            if (Player.Position.Y >= Player.Ladder?.Position.Y)
            {
                FSM.SetNextState(EPlayerState.CLIMB);
            }
        }
        else if (Player.CanGrip && Input.Up.Held)
        {
            FSM.SetNextState(EPlayerState.GRIP);
        }
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

    public override int TransitedAnimationIndex()
    {
        return Player.OnMomentum ? 1 : 0;
    }
}