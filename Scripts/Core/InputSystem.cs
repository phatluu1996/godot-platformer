using System.Collections.Generic;
using Godot;

public class InputSystem
{
    public InputKey Up = new InputKey("up");
    public InputKey Down = new InputKey("down");
    public InputKey Left = new InputKey("left");
    public InputKey Right = new InputKey("right");
    public InputKey Jump = new InputKey("jump");
    public InputKey Dash = new InputKey("dash");

    public List<InputKey> Keys;

    public InputSystem()
    {
        Keys = new List<InputKey>(){Up, Down, Left, Right, Jump, Dash};
    }

    public void Listen(){
        foreach(InputKey key in Keys)
        {
            key.Listen();
        }
    }

    public float xHAxis => Right.Held.GetHashCode() - Left.Held.GetHashCode();
    public float xPAxis => Right.Pressed.GetHashCode() - Left.Pressed.GetHashCode();
}