using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 150.0f;
	[Export] public float Health = 20.0f;
	[Export] public int ExperienceValue = 1;
	
	// We'll store a reference to the player here
	private Node2D _player;

	public override void _Ready()
	{
		// Find the player by their name in the World scene
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
	
	public void TakeDamage(float amount)
	{
		GD.Print("Enemy hit for: " + amount);
		Health -= amount;
		
		// Visual feedback
		Tween hitTween = CreateTween();
		Modulate = Colors.Red; 
		hitTween.TweenProperty(this, "modulate", Colors.White, 0.2f);
		
		// Check for death
		if (Health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GD.Print("Enemy Defeated!");
		QueueFree(); // Removes the enemy from the scene
	}
}
