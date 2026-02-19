using Godot;

public partial class ArrowProjectile : Area2D
{
	[Export] public float Speed = 400.0f;
	[Export] public float Damage = 10.0f;
	public Vector2 Direction = Vector2.Right;

	public override void _Process(double delta)
	{
		// Move in the assigned direction
		Position += Direction * Speed * (float)delta;
	}

	// Connect the Area2D "body_entered" signal to this
	public void OnBodyEntered(Node2D body)
	{
		GD.Print("Body Entered");
		if (body.HasMethod("TakeDamage")) 
		{
			body.Call("TakeDamage", Damage);
			QueueFree(); // Destroy bullet
		}
	}

	// Connect VisibleOnScreenNotifier2D "screen_exited" to this
	public void OnScreenExited() => QueueFree(); 
}
