using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using System.IO;

public class PlayerAnimationInitializer
{
    public static void LoadAnimations(PlayerAnimationController ac)
    {        
        string json = File.ReadAllText("./Json/zero_animation.json");

        Dictionary<EPlayerState, Dictionary<EPlayerWeapon, List<PlayerAnimation>>> stateAnimationsCollection = JsonConvert.DeserializeObject<Dictionary<EPlayerState, Dictionary<EPlayerWeapon, List<PlayerAnimation>>>>(json);

        ac.StateAnimations = stateAnimationsCollection; 

        Dictionary<string, PlayerAnimation> animationsMap = new Dictionary<string, PlayerAnimation>();
        foreach (var stateAnimations in stateAnimationsCollection.Values)
        {
            foreach (var _animations in stateAnimations.Values)
            {
                foreach (var animation in _animations)
                {
                    animationsMap.Add(animation.name, animation);
                }                
            }
        }    
        ac.AllAnimations = animationsMap;    
    }
}