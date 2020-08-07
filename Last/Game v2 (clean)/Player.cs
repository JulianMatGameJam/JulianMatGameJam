using Godot;
using System;
using Array = Godot.Collections.Array;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	//MOUVEMENT
	private const float gravity = 400.0f;
	private const int walkSpeed = 200;

	private Vector2 velocity;
	
	private void Move(float delta)
	{
		velocity.y += delta * gravity;
		
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			/*if (anim_priority == false && anim_cond == false)
				anim_mod = "Jump";*/
			velocity.y = -walkSpeed*0.9f;
		}
		
		
		if (Input.IsActionPressed("ui_left"))
		{
			/*if (anim_priority == false  && anim_cond == false)
				anim_mod = "Run";
			EmitSignal("WichWay","left");*/
			velocity.x = -walkSpeed;
			//anim_direction = "gauche";
		}
		else if (Input.IsActionPressed("ui_right"))
		{
			/*if (anim_priority == false  && anim_cond == false)
				anim_mod = "Run";
			EmitSignal("WichWay","right");*/
			velocity.x = walkSpeed;
			//anim_direction = "droite";
		}
		else 
		{
			/*if (anim_priority == false  && anim_cond == false)
				anim_mod = "Idle";*/
			velocity.x = 0;
		}

		MoveAndSlide(velocity, Vector2.Up, false, 4, Convert.ToSingle(Math.PI / 4), false);
	}
	
	//SIGNAUX
	private Vector2 LastPos = new Vector2();

	private bool IsAlive = true;
	
	private void _on_Checkpoint_body_entered(object body)
	{
		if (body == this)
			LastPos = Position;
	}
	
	private void _on_DeathSignal_body_entered(object body)
	{
		if (body == this)
			IsAlive = false;
	}
	
	private void _on_Signal_body_entered(object body)
	{
		Array boxs = GetTree().GetNodesInGroup("Pickable");
		foreach (Pickable box in boxs)
		{
			if (body == box)
				box.QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Move(delta);   
		var collision = MoveAndCollide(velocity * delta);
		if (collision != null)
			velocity = velocity.Slide(collision.Normal);

		//CHEAT
		if (Input.IsKeyPressed((int) KeyList.P))
			IsAlive = false;
		//CHEAT
		
		if (!IsAlive)
		{
			Position = LastPos;
			IsAlive = true;
		}
	}
}
