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
    public int startFrame = 0;
    public int repeatFrame = 0;
    public float repeatFrameProgess = 0f;
    public int resumeIndex = 0;
    public int resumeFrame = 0;
    public bool skipReplay = false;
    public bool canPlayNext = false;
    public int transitedFrame = 0;
    public Dictionary<(EPlayerState, int), Action<Player, PlayerState>> Events = new Dictionary<(EPlayerState, int), Action<Player, PlayerState>>();
    public Action<Player, PlayerState> OnFinishedEvent;
    public Action<Player, PlayerState> OnLoopedEvent;

    public PlayerAnimation(string n, int l = 0)
    {
        name = n;
        loopFrame = l;
    }

    public void AddEvent((EPlayerState, int) state_frame, Action<Player, PlayerState> evt)
    {
        if (!Events.ContainsKey(state_frame))
            Events.Add(state_frame, evt);
    }
}