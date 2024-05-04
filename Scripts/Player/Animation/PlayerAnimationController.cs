using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class PlayerAnimationController
{
    public AnimatedSprite2D AS;

    public Dictionary<string, PlayerAnimation> AllAnimations;
    public Dictionary<EPlayerState, Dictionary<EPlayerWeapon, List<PlayerAnimation>>> StateAnimations;

    public PlayerAnimationController(AnimatedSprite2D animatedSprite)
    {
        AS = animatedSprite;
        StateAnimations = new Dictionary<EPlayerState, Dictionary<EPlayerWeapon, List<PlayerAnimation>>>();
    }

    public Dictionary<EPlayerWeapon, List<PlayerAnimation>> GetState(EPlayerState state){
        return StateAnimations[state];
    }
}