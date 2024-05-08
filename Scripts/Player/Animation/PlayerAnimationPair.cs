using System;
using System.Collections.Generic;

[Serializable]
public class PlayerAnimationPair
{
    public List<PlayerAnimation> normal;
    public List<PlayerAnimation> special;

    public PlayerAnimationPair(){

    }

    public PlayerAnimationPair(List<PlayerAnimation> _normal, List<PlayerAnimation> _special)
    {
        normal = _normal;
        special = _special;
    }
}