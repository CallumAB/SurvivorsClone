using Godot;
using System.Collections.Generic;

public partial class Arrow : Node2D
{
	[Export] public PackedScene ProjectileScene; // Drag your Projectile.tscn here in the Inspector
	[Export] public float Range = 500.0f;

	// This is the function connected to your Timer's timeout signal
	public void OnTimerTimeout()
	{
		Node2D target = GetClosestEnemy();
		GD.Print("Weapon manager timer ticked");
		if (target != null)
		{
			
			GD.Print("Target Found: ", target.Name);
			Shoot(target.GlobalPosition);
		}
	}

	private void Shoot(Vector2 targetPos)
	{
		// 1. Create the bullet instance
		var bullet = ProjectileScene.Instantiate<ArrowProjectile>();

		// 2. Add it to the WORLD root (so it doesn't move with the player)
		GetTree().Root.AddChild(bullet);
		
		// 3. Set starting position
		bullet.GlobalPosition = GlobalPosition;
		
		// 4. Tell the bullet which way to fly
		Vector2 direction = (targetPos - GlobalPosition).Normalized();
		bullet.Direction = direction; 
	}

	private Node2D GetClosestEnemy()
	{
		var enemies = GetTree().GetNodesInGroup("Enemies");
		Node2D closest = null;
		float minDistance = Range;

		foreach (Node2D enemy in enemies)
		{
			float dist = GlobalPosition.DistanceTo(enemy.GlobalPosition);
			if (dist < minDistance)
			{
				minDistance = dist;
				closest = enemy;
			}
		}
		return closest;
	}
}
