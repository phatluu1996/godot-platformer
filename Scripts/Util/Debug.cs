using Godot;

public class Debug
{
    public static bool Enable = false;
    public static bool EnableDraw = true;
    public static void Log(object msg){
        if(Enable){
            GD.Print(msg);
        }
        
    }

    public static void Err(string err){
        if(Enable){
            GD.Print(err);
        }
    }

    public static void DrawRect(CanvasItem targetNode, Vector2 rectCenter, Vector2 rectSize, Color rectColor, bool filled = true, float width = -1){
        if(EnableDraw){
            targetNode.DrawRect(new Rect2(rectCenter - rectSize/2, rectSize), rectColor, filled, width);
        }
    }

    public static void DrawCircle(CanvasItem targetNode, Vector2 circleCenter, float circleRadius, Color circleColor, bool filled = true, float width = -1){
        if(EnableDraw){
            targetNode.DrawCircle(circleCenter, circleRadius, circleColor);
        }
    }
}