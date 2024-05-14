using System.Collections.Generic;
using System.Reflection.Metadata;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
        skipAnimationTransition = true;
    }

    public override void Enter()
    {
        base.Enter();
        Player.AC.TransitAnimationDefault(this);
        Player.velocity.Y = 0f;
        Player.velocity.X = -Player.Facing * Constants.KNOCKBACK_SPEED;
        Player.OnMomentum = false;
    }

    protected override void Update()
    {
        base.Update();
        Player.GravityForceApply();
        Timer += Player.GetPhysicsProcessDeltaTime();
        if (Timer > Constants.HURT_TIME)
        {
            if (Player.IsOnFloor())
            {
                FSM.SetNextState(EPlayerState.IDLE);
            }
            else
            {
                FSM.SetNextState(EPlayerState.FALL);
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
    }
    public override void OnAnimationLooped(string animationName)
    {
        base.OnAnimationLooped(animationName);
    }
}