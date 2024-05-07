using System.Collections.Generic;

public class PlayerFallState : PlayerAirBornState
{
    public PlayerFallState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        float movingSpeed = Player.OnMomentum ? Constants.MOMENTUM_SPEED : Constants.WALK_SPEED;
        Player.velocity.X = Input.xHAxis * movingSpeed;
        if (Input.xHAxis != 0 && (Player.IsOnWall() || Player.RaycastController.Collisions.Right || Player.RaycastController.Collisions.Left))
        {
            FSM.SetNextState(EPlayerState.WALLCLING);
        }
        else if (Player.IsOnFloor())
        {
            FSM.SetNextState(Input.xHAxis != 0 ? EPlayerState.WALK : EPlayerState.LAND);
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