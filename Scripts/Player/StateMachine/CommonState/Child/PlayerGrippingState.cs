using System.Collections.Generic;
using Godot;

public class PlayerGrippingState : PlayerState
{
    public PlayerGrippingState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, PlayerAnimationPair> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.velocity = Vector2.Zero;
        Player.OnMomentum = false;
        if (Player.GripableObject != null)
        {
            Player.y = Player.GripableObject.GlobalPosition.Y + Mathf.Abs(Player.GripTrigger.GlobalPosition.Y - Player.GlobalPosition.Y);
        }
        else
        {
            FSM.SetNextState(EPlayerState.FALL);
        }

    }

    protected override void Update()
    {
        base.Update();
        // Input.Listen();
        if (Input.xPAxis != 0 && Input.xPAxis != Player.Facing && !Player.IsAttacking)
        {
            Player.AC.PlayAnimation(Animation[EPlayerWeapon.NONE].normal[1]);
        }

        if (Input.Jump.Pressed)
        {
            if (Input.Down.Held)
            {
                Player.y += 4f;
                FSM.SetNextState(EPlayerState.FALL);
            }
            else if (!Input.Up.Held)
            {
                FSM.SetNextState(EPlayerState.JUMP);
            }
        }
        else if (Player.GripableObject == null)
        {
            FSM.SetNextState(EPlayerState.FALL);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
        if (frame == 4 && Animation[EPlayerWeapon.NONE].normal[1].name == Player.AS.Animation)
        {
            Player.FlipH();
        }
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