using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class PlayerGroundedState : PlayerState
{    
    public PlayerGroundedState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation) : base(player, fsm, animation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.OnMomentum = false;
    }

    protected override void Update()
    {
        base.Update();
        Input.Listen();
        if (Input.xHAxis != 0)
        {
            Player.Facing = Input.xHAxis;
            Player.AS.FlipH = Player.Facing < 0;
        }

        Player.GravityForceApply();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnFrameChangedEvent(int frame)
    {
        base.OnFrameChangedEvent(frame);
    }
}