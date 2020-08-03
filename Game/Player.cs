using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	private Sprite Screamer()
	{
		return GetNode<Node2D>("Screamer").GetNode<Sprite>("Sprite");
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	

	private const float gravity = 400.0f;
	private const int walkSpeed = 200;

	private Vector2 velocity;
	private Vector2 _velocity;
	
	private void Move(float delta)
	{
		velocity.y += delta * gravity;

		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
			velocity.y = -walkSpeed*0.85f;
		
		if (Input.IsActionPressed("ui_left"))
			velocity.x = -walkSpeed;
		else if (Input.IsActionPressed("ui_right"))
			velocity.x = walkSpeed;
		else
			velocity.x = 0;
		
		MoveAndSlide(velocity, Vector2.Up, true, 4, Convert.ToSingle(Math.PI / 4), false);
	}

	private void _on_Signal_body_entered(object body)
	{
		if (body == this)
		{
			GD.Print("oui");
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Move(delta);
		var collision = MoveAndCollide(velocity * delta);
		if (collision != null)
			velocity = velocity.Slide(collision.Normal);
		
	}
}
