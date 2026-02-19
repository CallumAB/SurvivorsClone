using Godot;
using System.Linq;

public partial class WeaponBase : Node2D
{
	[Export] public PackedScene ProjectileScene;
	[Export] public float AttackRange = 500.0f;

	public void Fire()
	{
		Node2D target = GetClosestEnemy();
		if (target == null) return;

		// Instantiate bullet
		var bullet = ProjectileScene.Instantiate<ArrowProjectile>();
		
		// Always add projectiles to the World root, not the player!
		GetTree().Root.AddChild(bullet);
		
		bullet.GlobalPosition = GlobalPosition;
		bullet.Direction = (target.GlobalPosition - GlobalPosition).Normalized();
	}

	private Node2D GetClosestEnemy()
	{
		var enemies = GetTree().GetNodesInGroup("Enemies");
		Node2D closest = null;
		float minDistance = AttackRange;

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
