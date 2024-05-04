using System.Collections.Generic;
using Godot;

public class PlayerDashState : PlayerGroundedState
{
    public bool initDash;
    public bool endDashSoon;

    public PlayerDashState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        initDash = true;
        endDashSoon = false;
        Player.velocity.X = Player.Facing * Constants.PREDASH_SPEED;
    }

    public override void Update()
    {
        base.Update();

        Timer += (float)Player.GetProcessDeltaTime();

        if (Timer >= Constants.DASH_TIME || Input.Dash.Released)
        {
            if (Input.xHAxis == 0)
            {
                if (Player.AS.Animation == Animation[EPlayerWeapon.NONE][0].name || Player.AS.Animation == Animation[EPlayerWeapon.NONE][1].name)
                {
                    if (Timer < Constants.DASH_TIME)
                    {
                        endDashSoon = true;
                        Timer = Constants.DASH_TIME;
                    }
                    Player.PlayAnimation(Animation[EPlayerWeapon.NONE][2].name);
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
                FSM.SetNextState(EPlayerState.WALK);
                return;
            }

        }
        else if (Timer >= Constants.PREDASH_TIME && Timer < Constants.DASH_TIME)
        {
            Player.velocity.X = Player.Facing * Constants.DASH_SPEED;
        }

        if (Input.Dash.Pressed)
        {
            endDashSoon = false;
            Player.PlayAnimation(Animation[EPlayerWeapon.NONE][0].name);
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
    }

    public override void Exit()
    {
        base.Exit();
        initDash = false;
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
    }

    public override void OnAnimationFinished(string animationName)
    {
        base.OnAnimationFinished(animationName);
        if (animationName == Animation[EPlayerWeapon.NONE][0].name)
        {
            initDash = false;
            Player.PlayAnimation(Animation[EPlayerWeapon.NONE][1].name);
        }
        else if (animationName == Animation[EPlayerWeapon.NONE][2].name)
        {
            FSM.SetNextState(Input.xHAxis != 0 ? EPlayerState.WALK : EPlayerState.IDLE);
        }
    }
}