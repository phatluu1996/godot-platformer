using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

public class PlayerWalkState : PlayerGroundedState
{
    public float Speed;
    public PlayerWalkState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
        Player.velocity.X = Input.xHAxis * Speed;
        if (!Player.IsOnFloor())
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Input.Jump.Pressed)
        {
            FSM.SetNextState(EPlayerState.JUMP);
        }
        else if (Input.Dash.Pressed)
        {
            FSM.SetNextState(EPlayerState.DASH);
        }
        else if (Input.xHAxis == 0)
        {
            FSM.SetNextState(EPlayerState.IDLE);
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