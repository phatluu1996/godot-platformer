using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class PlayerAnimationController
{
    public PlayerAnimation Animation;
    public Player Player;
    public AnimatedSprite2D AS;
    public Dictionary<(EPlayerState, string), PlayerAnimation> AllAnimations;
    public Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>> StateAnimations;

    public PlayerAnimationController(AnimatedSprite2D animatedSprite, Player player)
    {
        StateAnimations = new Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>>();
        Player = player;
        AS = animatedSprite;
        AS.AnimationChanged += () => OnAnimationChanged(Animation);
		AS.FrameChanged += OnFrameChangedEvent;
		AS.AnimationFinished += OnAnimationFinished;
		AS.AnimationLooped += OnAnimationLooped;
    }

    public Dictionary<EPlayerWeapon, PlayerAnimationPair> GetState(EPlayerState state){
        return StateAnimations[state];
    }

    public PlayerAnimation GetAnimation((EPlayerState, string) state_name){
        return AllAnimations[state_name];
    }

    public PlayerAnimationPair GetAnimationPair(EPlayerState state, EPlayerWeapon weapon){
        if(StateAnimations.ContainsKey(state)){
            if(StateAnimations[state].ContainsKey(weapon)){
                return StateAnimations[state][weapon];
            }
        }
        return null;
    }

    public void PlayAnimation(PlayerAnimation animation, int frame = 0, float frameProgress = 0, bool playBack = false){
        if(frame == -1){
			frame = AS.Frame;
			frameProgress = AS.FrameProgress;
		}
        Animation = animation;
		AS.Play(animation.name, fromEnd: playBack);		
		AS.SetFrameAndProgress(frame, frameProgress);
    }

    public bool IsAnimationFinished() //Only work with no-loop animation
	{
		int frameCount = AS.SpriteFrames.GetFrameCount(AS.Animation) - 1;
		return AS.Frame >= frameCount && AS.FrameProgress == 1f;
	}

	public void OnAnimationChanged(PlayerAnimation animation)
	{
        
	}

	private void OnFrameChangedEvent()
	{
        int frame = AS.Frame;
        if(Animation.Events.ContainsKey((Animation.state, frame))){
            Animation.Events[(Animation.state, frame)].Invoke(Player, Player.FSM.CurrentState as PlayerState);
        }
        
        //On Frame change setting for state 
		Player.FSM.OnFrameChangedEvent(frame);
	}

	private void OnAnimationFinished()
	{
        if(Animation.OnFinishedEvent != null){
            Animation.OnFinishedEvent.Invoke(Player, Player.FSM.CurrentState as PlayerState);
        }
        //On animation finished setting for state 
		Player.FSM.OnAnimationFinished(Animation.name);
	}

	private void OnAnimationLooped()
	{
        if(Animation.OnLoopedEvent != null){
            Animation.OnLoopedEvent.Invoke(Player, Player.FSM.CurrentState as PlayerState);
        }
        //On animation loop setting for state         
		int loopFrame = Animation.loopFrame;
		if (loopFrame != 0)
		{
			AS.SetFrameAndProgress(loopFrame, 0);
		}        
		Player.FSM.OnAnimationLooped(Animation.name);
	}
}