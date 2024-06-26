using System.Collections.Generic;

public class PlayerJumpState : PlayerAirBornState
{
    public PlayerJumpState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity.Y = -Constants.JUMP_FORCE;
    }

    protected override void Update()
    {
        base.Update();
        float movingSpeed = Player.OnMomentum ? Constants.MOMENTUM_SPEED : Constants.WALK_SPEED;
        Player.velocity.X = Input.xHAxis * movingSpeed;
        if (Player.velocity.Y >= 0f || !Input.Jump.Held || Player.IsOnCeilingOnly())
        {
            Player.velocity.Y = 0;
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Player.CanClimbLadder && (Input.Up.Pressed || Input.Down.Pressed))
        {
            FSM.SetNextState(EPlayerState.CLIMB);
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
}