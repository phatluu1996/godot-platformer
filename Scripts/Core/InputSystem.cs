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
    public InputKey Attack = new InputKey("attack");
    public InputKey SubAttack = new InputKey("sub_attack");
    public InputKey LMenu = new InputKey("left_menu");
    public InputKey RMenu = new InputKey("right_menu");
    public InputKey Start = new InputKey("start");
    public InputKey Select = new InputKey("select");


    public List<InputKey> Keys;

    public InputSystem()
    {
        Keys = new List<InputKey>(){Up, Down, Left, Right, Jump, Dash, Attack, SubAttack, LMenu, RMenu, Start, Select};
    }

    public void Listen(){
        foreach(InputKey key in Keys)
        {
            key.Listen();
        }
    }

    public float xHAxis => Right.Held.GetHashCode() - Left.Held.GetHashCode();
    public float xPAxis => Right.Pressed.GetHashCode() - Left.Pressed.GetHashCode();
    public float yHAxis => Down.Held.GetHashCode() - Up.Held.GetHashCode();
    public float yPAxis => Down.Pressed.GetHashCode() - Up.Pressed.GetHashCode();
}