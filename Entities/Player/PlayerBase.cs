using Godot;

public partial class PlayerBase : CharacterBody2D
{
	// [Export] makes this variable editable in the Godot Inspector
	[Export] public float Speed = 400.0f;

	public override void _PhysicsProcess(double delta)
	{
		// 1. Get the direction vector based on the default Godot UI actions (WASD/Arrows)
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		// 2. Velocity is a built-in property of CharacterBody2D
		// We multiply the direction by our speed
		Velocity = direction * Speed;

		// 3. MoveAndSlide handles the math of moving and colliding with walls
		MoveAndSlide();
	}
}
