using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class PlayerAnimation
{
    public int length = 0;
    public EPlayerWeapon type;
    public EPlayerState state;

    public string name;
    public int loopFrame = 0;

    //-1: current frame, -2: last frame
    public int startFrame = 0;
    public float startProgress = 0;



    public bool repeat = false;
    //-1: current frame, -2: last frame
    public int repeatFrame = 0;

    public float repeatProgress = 0;


    //-1: current frame, -2: last frame
    public int replayFrame = 0;
    public float replayProgress = 0;


    public int resumeIndex = 0;
    //-1: current frame, -2: last frame
    public int resumeFrame = 0;
    public float resumeProgress = 0;

    public int forwardFrame = 0;
    public float forwardProgress = 0;

    public PlayerAnimation(string n, int l = 0)
    {
        name = n;
        loopFrame = l;
    }
}

