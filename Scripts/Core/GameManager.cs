using Godot;
using System;

public partial class GameManager : Node
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Input.IsActionJustPressed("toggle_debug")){
            Debug.Enable = !Debug.Enable;
        }
    }
}
