using System;
using System.Collections.Generic;

[Serializable]
public class PlayerAnimation
{
    public EPlayerWeapon type;
    public EPlayerState state;
    public string name;
    public int loopFrame;    
    public int startedFrame = 0;
    public int repeatFrame = 0;
    public int transitedFrame = 0;
    public int continueFrame = 0;
    public List<Action> FrameEvents;
    public Action AnimationFinishedEvent;

    public PlayerAnimation(string n, int l = 0)
    {
        name = n;
        loopFrame = l;
    }
}