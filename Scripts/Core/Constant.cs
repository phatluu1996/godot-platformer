using Godot;
using System;

public class Constants
{
	public static readonly float JUMP_FORCE = 5.25f * Engine.MaxFps;
	public static readonly float GRAVITY = 0.25f * Engine.MaxFps * Engine.MaxFps;
	public static readonly float MAX_WALL_GRAVITY = 1.25f * Engine.MaxFps;
	public static readonly float PREWALK_SPEED = 0.5f * Engine.MaxFps;
	public static readonly float WALK_SPEED = 1.35f * Engine.MaxFps;
	public static readonly float CLIMB_SPEED = 1f * Engine.MaxFps;
	public static readonly float PREDASH_SPEED = 0.5f * Engine.MaxFps;
	public static readonly float DASH_SPEED = 3.25f * Engine.MaxFps;
	public static readonly float ENDDASH_SPEED = 0.5f * Engine.MaxFps;
	public static readonly float KNOCKBACK_SPEED = 0.5f * Engine.MaxFps;
	public static readonly float HURT_TIME = 0.3f;
	public static readonly float DASH_TIME = 0.55f;
	public static readonly float PREDASH_TIME = 0.05f;
	public static readonly float MOMENTUM_SPEED = 2.75f * Engine.MaxFps;
	public static readonly Vector2 STAND_BOX_SIZE = new Vector2(18, 38);
	public static readonly Vector2 STAND_BOX_OFFSET = new Vector2(0, -19);
	public static readonly Vector2 DASH_BOX_SIZE = new Vector2(18, 24);
	public static readonly Vector2 DASH_BOX_OFFSET = new Vector2(0, -12);

}
