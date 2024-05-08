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
    public float repeatFrameProgess = 0f;
    public int transitedFrame = 0;
    public int resumeIndex = 0;
    public int resumeFrame = 0;
    public bool cancelOnTransiting = false;
    public bool skipOnTransited = false;    
    public bool canPlayNext = false;
    public List<Action> FrameEvents;
    public Action AnimationFinishedEvent;

    public PlayerAnimation(string n, int l = 0)
    {
        name = n;
        loopFrame = l;
    }
}