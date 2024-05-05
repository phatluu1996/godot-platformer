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
    public double Timer;

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
            Player.OnAnimationTransited(this);
        }
    }

    public override void Update()
    {
        
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

    public override string TransitedAnimation(){        
        return Animation[EPlayerWeapon.NONE][0].name;
    }
}