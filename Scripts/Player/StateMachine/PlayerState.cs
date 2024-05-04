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
    public Dictionary<EPlayerWeapon, List<PlayerAnimation>> Animation;
    public float Timer;

    public PlayerState(Player player, PlayerStateMachine fsm, Dictionary<EPlayerWeapon, List<PlayerAnimation>> animation)
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
            Player.AnimationTransition(Animation);
        }
    }

    public override void Exit()
    {

    }

    public override void OnFrameChangedEvent(int frame)
    {
        // GD.Print(frame);
    }

    public override void OnAnimationFinished(string animationName)
    {
        // GD.Print(animationName);
    }
    public override void OnAnimationLooped(string animationName)
    {
        // GD.Print(animationName);
    }
    public override void Update()
    {

    }
}