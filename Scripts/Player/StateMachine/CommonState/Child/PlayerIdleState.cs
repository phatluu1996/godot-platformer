using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity.X = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (!Player.IsOnFloor())
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Input.Jump.Pressed)
        {
            FSM.SetNextState(EPlayerState.JUMP);
        }
        else if (Input.Dash.Pressed && !Player.RaycastController.Collisions.Right && !Player.RaycastController.Collisions.Left)
        {
            FSM.SetNextState(EPlayerState.DASH);
        }
        else if (Input.xHAxis != 0 && !Player.IsAttacking && !Player.RaycastController.Collisions.Right && !Player.RaycastController.Collisions.Left)
        {
            FSM.SetNextState(EPlayerState.WALK);
        }
        else if (Player.CanClimbLadder && Input.Up.Pressed)
        {
            if (Player.Position.Y >= Player.Ladder?.Position.Y)
            {
                FSM.SetNextState(EPlayerState.CLIMB);
            }
        }
        else if (Player.CanClimbLadder && Input.Down.Pressed)
        {
            if (Player.Position.Y < Player.Ladder?.Position.Y)
            {
                FSM.SetNextState(EPlayerState.CLIMBDOWN);
            }
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
        if (StateKey == EPlayerState.LAND)
        {
            FSM.SetNextState(EPlayerState.IDLE);
        }
    }

    public override string TransitedAnimation()
    {
        if (Player.Energy < Player.EnergyLimit / 3)
        {
            return Animation[EPlayerWeapon.NONE].normal[1].name;
        }
        return Animation[EPlayerWeapon.NONE].normal[0].name;
    }
}