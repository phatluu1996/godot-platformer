using Godot;
using System;

public class Constants
{
	public static float JUMP_FORCE = 320f;
	public static float GRAVITY = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public static float PREWALK_SPEED = 40f;
	public static float WALK_SPEED = 80f;
	public static float PREDASH_SPEED = 40f;
	public static float DASH_SPEED = 200f;
	public static float ENDDASH_SPEED = 30f;
	public static float DASH_TIME = 0.55f;
	public static float PREDASH_TIME = 0.05f;
	public static float MOMENTUM_SPEED = 180f;
}
