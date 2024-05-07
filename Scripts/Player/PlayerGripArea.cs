using Godot;
using System;

public partial class PlayerGripArea : Area2D
{
	[Export]
	public Player Player;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		AreaExited += OnAreaExited;
	}

	public void OnAreaEntered(Area2D area){
		Player.CanGrip = true;
		Player.GripableObject = area;
	}

	public void OnAreaExited(Area2D area){
		Player.CanGrip = false;
		Player.GripableObject = null;
	}
}
