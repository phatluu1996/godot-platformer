using System.Collections.Generic;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
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
        float movingSpeed = Player.OnMomentum ? Constants.MOMENTUM_SPEED : Constants.WALK_SPEED;
        Player.GravityForceApply();
        Timer += Player.GetProcessDeltaTime();
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
            Input.Listen();
            if (Input.xHAxis != 0)
            {
                Player.Facing = Input.xHAxis;
                Player.AS.FlipH = Player.Facing < 0;
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
        }else if(Player.CanGrip && Input.Up.Held){
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

    public override string TransitedAnimation()
    {
        int index = Player.OnMomentum ? 1 : 0;
        return Animation[EPlayerWeapon.NONE][index].name;
    }
}