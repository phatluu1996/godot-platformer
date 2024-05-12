using System;
using Godot;

public class PlayerAnimationEventInitializer
{
    PlayerAnimationController ac;

    private PlayerAnimationEventInitializer(PlayerAnimationController _ac)
    {
        ac = _ac;
    }

    public static void LoadAnimationEvents(PlayerAnimationController _ac){
        new PlayerAnimationEventInitializer(_ac).AddAnimationEvent();
    }

    private void AddEventN(EPlayerState state, EPlayerWeapon weapon, int index, int frame, Action<Player, PlayerState> evt)
    {
        PlayerAnimationPair aPair = ac.GetAnimationPair(state, weapon);
        if (aPair != null)
        {
            if (frame < aPair.normal[index].length)
            {
                aPair.normal[index].AddEvent((state, frame), evt);
            }
            else
            {
                GD.Print("AddEventN() : Cannot add event in this frame: " + frame);
            }
        }else{
            GD.Print("AddEventN() : event doesn't exist");
        }
    }

    private void AddEventS(EPlayerState state, EPlayerWeapon weapon, int index, int frame, Action<Player, PlayerState> evt)
    {
        PlayerAnimationPair aPair = ac.GetAnimationPair(state, weapon);
        if (aPair != null)
        {
            
            if (frame < aPair.special[index].length)
            {
                aPair.special[index].AddEvent((state, frame), evt);
            }
            else
            {
                GD.Print("AddEventS() : Cannot add event in this frame: " + frame);
            }
        }
        else
        {
            GD.Print("AddEventS() : event doesn't exist");
        }

    }

    private void AddFinishedEventN(EPlayerState state, EPlayerWeapon weapon, int index, Action<Player, PlayerState> evt)
    {
        PlayerAnimationPair aPair = ac.GetAnimationPair(state, weapon);
        if (aPair != null)
        {
            if(!ac.AS.SpriteFrames.GetAnimationLoop(aPair.normal[index].name)){
                aPair.normal[index].OnFinishedEvent = evt;
            }else{
                GD.Print("Animation doesn't configured as loop");
            }
            
        }
        else
        {
            GD.Print("AddFinishedEventN() : event doesn't exist");
        }
    }

    private void AddFinishedEventS(EPlayerState state, EPlayerWeapon weapon, int index, Action<Player, PlayerState> evt)
    {
        PlayerAnimationPair aPair = ac.GetAnimationPair(state, weapon);
        if (aPair != null)
        {
            if(!ac.AS.SpriteFrames.GetAnimationLoop(aPair.special[index].name)){
                aPair.special[index].OnFinishedEvent = evt;
            }else{
                GD.Print("Animation doesn't configured as loop");
            }
        }
        else
        {
            GD.Print("AddFinishedEventS() : event doesn't exist");
        }
    }

    public void AddAnimationEvent()
    {

    }
}