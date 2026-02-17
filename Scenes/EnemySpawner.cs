using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	// This allows us to drag the Enemy.tscn into the slot in the Inspector
	[Export] public PackedScene EnemyScene;
	[Export] public float SpawnRadius = 600.0f;

	public void OnTimerTimeout()
	{
		// Create an instance of the enemy
		Enemy enemyInstance = EnemyScene.Instantiate<Enemy>();

		// Pick a random direction (0 to 2π radians)
		float randomAngle = (float)GD.RandRange(0, Math.PI * 2);
		Vector2 spawnOffset = new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle)) * SpawnRadius;

		// Set position relative to the player
		var player = GetTree().Root.FindChild("PlayerBase", true, false) as Node2D;
		
		if (player != null)
		{
			enemyInstance.GlobalPosition = player.GlobalPosition + spawnOffset;
			// Add the enemy to the scene
			GetTree().Root.AddChild(enemyInstance);
		}
	}
}
