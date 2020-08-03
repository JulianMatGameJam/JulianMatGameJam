using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

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

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			anim_mod = "Jump";
			velocity.y = -walkSpeed*0.85f;
		}

		if (Input.IsActionPressed("ui_left"))
		{
			anim_mod = "Run";
			velocity.x = -walkSpeed;
			anim_direction = "gauche";
		}
			
		else if (Input.IsActionPressed("ui_right"))
		{
			anim_mod = "Run";
			velocity.x = walkSpeed;
			anim_direction = "droite";
		}

		else
		{
			anim_mod = "Idle";
			velocity.x = 0;
		}
			
		
		MoveAndSlide(velocity, Vector2.Up, true, 4, Convert.ToSingle(Math.PI / 4), false);
	}
	
	private void _on_Signal_body_entered(object body)
	{
		GD.Print("oaie");
	}
	
	// Animation 
	
	private string anim_mod = "Idle";
	private string anim_direction = "droite";
	private float anim_speed = 1;
	
	
	
	private void AnimationLoop()
	{
		GetNode<AnimationPlayer>("AnimatedSprite/AnimationPlayer").Play(anim_mod + "_" + anim_direction, -1,anim_speed);
	}


	
	
	public override void _PhysicsProcess(float delta)
	{
		AnimationLoop();
	}


	public override void _Process(float delta)
	{
		Move(delta);
		var collision = MoveAndCollide(velocity * delta);
		if (collision != null)
			velocity = velocity.Slide(collision.Normal);
	}
}
