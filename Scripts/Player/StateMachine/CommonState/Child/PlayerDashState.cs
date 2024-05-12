using System.Collections.Generic;
using System.Numerics;
using Godot;

public class PlayerDashState : PlayerGroundedState
{
    public bool wasAttacked;
    public bool initDash;
    public bool endDashSoon;

    public PlayerDashState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        initDash = true;
        wasAttacked = false;
        endDashSoon = false;
        Player.velocity.X = Player.Facing * Constants.PREDASH_SPEED;
        Player.CollisionBox.Size = Constants.DASH_BOX_SIZE;
        Player.CS.Position = Constants.DASH_BOX_OFFSET;
    }

    protected override void Update()
    {
        base.Update();

        Timer += Player.GetProcessDeltaTime();

        if (Timer >= Constants.DASH_TIME || Input.Dash.Released)
        {
            if (Input.xHAxis == 0)
            {
                if (Player.AC.Animation.name == Animation[EPlayerWeapon.NONE].normal[0].name || Player.AC.Animation.name == Animation[EPlayerWeapon.NONE].normal[1].name)
                {
                    if (Timer < Constants.DASH_TIME)
                    {
                        endDashSoon = true;
                        Timer = Constants.DASH_TIME;
                    }
                    Player.AC.PlayAnimation(Animation[EPlayerWeapon.NONE].normal[2]);
                }
                float endDashSpeed = Constants.ENDDASH_SPEED;
                if (endDashSoon)
                {
                    endDashSpeed = Constants.ENDDASH_SPEED / 3;
                }
                Player.velocity.X = Player.Facing * endDashSpeed;
            }
            else
            {
                if(Player.CanWalkWhenAttacking()){
                    FSM.SetNextState(EPlayerState.WALK);
                    return;
                }                
            }

        }
        else if (Timer >= Constants.PREDASH_TIME && Timer < Constants.DASH_TIME)
        {
            Player.velocity.X = Player.Facing * Constants.DASH_SPEED;
        }

        if (Input.Dash.Pressed)
        {
            endDashSoon = false;
            Player.AC.PlayAnimation(Animation[EPlayerWeapon.NONE].normal[0]);
            Timer = 0f;
        }

        if (!Player.IsOnFloor())
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
        else if (Input.Jump.Pressed)
        {
            Player.OnMomentum = Input.Dash.Held;
            FSM.SetNextState(EPlayerState.JUMP);
        }
        else if (Player.IsOnWall())
        {
            FSM.SetNextState(EPlayerState.IDLE);
        }
    }

    public override void Exit()
    {
        base.Exit();
        initDash = false;
        Player.CollisionBox.Size = Constants.STAND_BOX_SIZE;
        Player.CS.Position = Constants.STAND_BOX_OFFSET;
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
        if (animationName == Animation[EPlayerWeapon.NONE].normal[0].name)
        {
            initDash = false;
            Player.AC.PlayAnimation(Animation[EPlayerWeapon.NONE].normal[1]);
        }
        else if (animationName == Animation[EPlayerWeapon.NONE].normal[2].name)
        {
            FSM.SetNextState(Input.xHAxis != 0 ? EPlayerState.WALK : EPlayerState.IDLE);
        }
    }
}