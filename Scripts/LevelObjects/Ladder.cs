using Godot;
using System;

public partial class Ladder : Area2D
{
	[Export]
	public CollisionShape2D CS;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D node)
	{
		if (node is Player)
		{
			Player player = (node as Player);
			player.CanClimbLadder = true;
			player.Ladder = this;
		}
		else
		{

		}
	}

	public void OnBodyExited(Node2D node)
	{
		if (node is Player)
		{
			Player player = (node as Player);
			player.CanClimbLadder = false;
			player.Ladder = null;
		}
		else
		{

		}

	}
}
