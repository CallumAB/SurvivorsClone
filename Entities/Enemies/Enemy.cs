using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 150.0f;
	
	// We'll store a reference to the player here
	private Node2D _player;

	public override void _Ready()
	{
		// For now, we find the player by their name in the World scene
		// In a bigger project, we'd use a more robust system
		_player = GetTree().Root.FindChild("PlayerBase", true, false) as Node2D;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_player == null) return;

		// Vector Math: Direction = Target - Self
		Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
		
		Velocity = direction * Speed;
		MoveAndSlide();
	}
}
