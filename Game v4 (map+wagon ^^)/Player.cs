using Godot;
using System;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private bool alive = true;
	
	private const float gravity = 400.0f;
	private const int walkSpeed = 300;

	private Vector2 velocity;
	private Vector2 _velocity;
	
	private void Move(float delta)
	{
		velocity.y += delta * gravity;

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			if (anim_priority == false && anim_cond == false)
			{
				anim_mod = "Jump";
			}
			velocity.y = -walkSpeed*0.85f;
		}

		if (Input.IsActionPressed("ui_left"))
		{
			if (anim_priority == false  && anim_cond == false)
			{
				anim_mod = "Run";
			}
			EmitSignal("WichWay","left");
			velocity.x = -walkSpeed;
			anim_direction = "gauche";
		}
			
		else if (Input.IsActionPressed("ui_right"))
		{
			if (anim_priority == false  && anim_cond == false)
			{
				anim_mod = "Run";
			}
			EmitSignal("WichWay","right");
			velocity.x = walkSpeed;
			anim_direction = "droite";
		}

		else 
		{
			if (anim_priority == false  && anim_cond == false)
			{
				anim_mod = "Idle";
			}
			velocity.x = 0;
		}
		
			
		
		MoveAndSlide(velocity, Vector2.Up, true, 4, Convert.ToSingle(Math.PI / 4), false);
		int index = GetSlideCount();
		for (int i = 0; i < index; i++)
		{
			var collision = GetSlideCollision(i);

			Array arraybox = GetTree().GetNodesInGroup("Box");
			Array arraywagons = GetTree().GetNodesInGroup("Wagons");
			foreach (Box box in arraybox)
			{
				if (collision.Collider == box)
				{
					box.ApplyCentralImpulse(-collision.Normal * 100);
					if (box.Position.y <= Position.y)
					{
						anim_mod = "Push";
					}
					
				}
			}	
			foreach (Wagon wagon in arraywagons)
			{
				if (collision.Collider == wagon)
				{
					wagon.ApplyCentralImpulse(-collision.Normal * 100);
					if (wagon.Position.y <= Position.y)
					{
						anim_mod = "Push";
					}
					
				}
			}
		}
		

	}
	
	[Signal]
	public delegate void WichWay(string way);
	
	private void _on_Signal_body_entered(object body)
	{
		if (body == this)
			alive = false;
	}
	
	private Vector2 lastpos = new Vector2();
	
	private void _on_Checkpoint_body_entered(object body)
	{
		if (body == this)
			lastpos = Position;
	}
	
	private void _on_BoxSignal_body_entered(object body)
	{
		Array boxlist = GetTree().GetNodesInGroup("Box");
		foreach (Box b in boxlist)
		{
			if (body == b)
				b.QueueFree();
		}
	}
	
	private void _on_Area2D2_body_entered(object body)
	{
		if (body == this)
			alive = false;
	}
	
	private void _on_SpiderSignal_body_entered(object body)
	{
		Spider spider = (Spider) GetTree().GetNodesInGroup("Spider")[0];
		if (body == this)
			spider.Position = new Vector2(6365, 472);
	}
	
	// Animation ---------------------------------------------------------------------------------
	
	private string anim_mod = "Idle";
	private string anim_direction = "droite";
	private float anim_speed = 1;
	private bool anim_priority = false;
	private bool anim_cond = false;

	
	private void _on_PickableObject_IsGrapping(string cond)
	{
		if (cond == "2")
		{
			anim_priority = true;
			anim_mod = "Push";
		}
		else if (cond == "1")
		{
			anim_cond = true;
			anim_mod = "Throw";
		}
		else
		{
			anim_priority = false;
		}
	}
	
	private void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		if (anim_name == "Throw_droite" || anim_name == "Throw_gauche")
		{
			anim_cond = false;
		}
	}

	
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
		/*var collision = MoveAndCollide(velocity * delta);
		if (collision != null)
			velocity = velocity.Slide(collision.Normal);*/


		//pour le test de respawn
		if (Input.IsKeyPressed((int) KeyList.P))
			alive = false;

		/*int index = GetSlideCount();
		for (int i = 0; i < index; i++)
		{
			var collision = GetSlideCollision(i);
			if (collision.Collider == this)
				
		}
		*/
		if (!alive)
		{
			alive = true;
			Position = lastpos;
		}
	}
}
