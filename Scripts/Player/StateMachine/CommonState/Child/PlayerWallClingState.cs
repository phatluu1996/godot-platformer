using System.Collections.Generic;

public class PlayerWallClingState : PlayerState
{
    public PlayerWallClingState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.OnMomentum = false;
    }

    public override void Update()
    {
        base.Update();
        Input.Listen();
        Player.GravityForceApply(-Constants.MAX_WALL_GRAVITY, Constants.MAX_WALL_GRAVITY);
        
        if(Player.IsOnFloor()){
            FSM.SetNextState(EPlayerState.IDLE);
        }else if(Input.xHAxis == 0 || Input.xHAxis != Player.Facing || (!Player.RaycastController.Collisions.Right && !Player.RaycastController.Collisions.Left)){
            FSM.SetNextState(EPlayerState.FALL);
        }else if(Input.Jump.Pressed){
            Player.OnMomentum = Input.Dash.Held;
            FSM.SetNextState(EPlayerState.WALLJUMP);
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
    public override void OnAnimationLooped(string animationName)
    {
        base.OnAnimationLooped(animationName);
    }
}