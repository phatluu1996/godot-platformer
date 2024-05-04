using Godot;

public class InputKey
{
    public Key KeyCode;
    private string Action;
    public bool Pressed;
    public bool Held;
    public bool Released;

    public InputKey(string action)
    {        
        Action = action;
        Pressed = Held = Released = false;
    }
    public void Reset(){
        Pressed = Held = Released = false;
    }

    public void Listen(){
        Pressed = Input.IsActionJustPressed(Action);
        Held = Input.IsActionPressed(Action);
        Released = Input.IsActionJustReleased(Action);
    }
}