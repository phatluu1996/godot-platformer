using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class PlayerState : BaseState<EPlayerState>
{
    public Player Player;
    public PlayerStateMachine FSM;
    public InputSystem Input;
    public Dictionary<EPlayerWeapon, PlayerAnimationPair> Animation;
    public double Timer;

    public PlayerState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation)
    {
        Player = player;
        FSM = fsm;
        Input = player.Input;
        Animation = animation;
    }

    public override void Enter()
    {
        Timer = 0;
        if (Animation != null)
        {
            Player.OnAnimationTransited(this);
        }
    }

    public override void Excute()
    {
        PreUpdate();
        Update();
        PostUpdate();
    }

    protected override void PreUpdate()
    {

    }

    protected override void Update()
    {

    }

    protected override void PostUpdate()
    {
        Player.LogicUpdate(this);
    }

    public override void Exit()
    {

    }

    public override void OnFrameChangedEvent(int frame)
    {

    }

    public override void OnAnimationFinished(string animationName)
    {

    }
    public override void OnAnimationLooped(string animationName)
    {

    }

    public override string TransitedAnimation()
    {
        return Animation[EPlayerWeapon.NONE].normal[0].name;
    }

    public virtual void OnAtackAnimationTransited()
    {
        Player.IsAttacking = false;
        Player.PlayAnimation(TransitedAnimation(), 0, 0);
    }

    public virtual void OnAttackAnimationEnd()
    {
        Player.IsAttacking = false;
        Player.AttackIndex = 0;
        Player.PlayAnimation(ResumeAttackingAnimation(), ResumedAnimationFrame(), 0);
    }

    public virtual string ResumeAttackingAnimation()
    {
        PlayerAnimation animation = Player.GetAnimation(Player.AS.Animation);
        return Animation[EPlayerWeapon.NONE].normal[animation.resumeIndex].name;
    }

    public virtual int ResumedAnimationFrame()
    {
        return Animation[EPlayerWeapon.NONE].normal[0].resumeFrame;
    }
}