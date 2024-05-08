using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class PlayerAnimationController
{
    public AnimatedSprite2D AS;

    public Dictionary<string, PlayerAnimation> AllAnimations;
    public Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>> StateAnimations;

    public PlayerAnimationController(AnimatedSprite2D animatedSprite)
    {
        AS = animatedSprite;
        StateAnimations = new Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>>();
    }

    public Dictionary<EPlayerWeapon, PlayerAnimationPair> GetState(EPlayerState state){
        return StateAnimations[state];
    }

    public PlayerAnimation GetAnimation(string name){
        return AllAnimations[name];
    }
}