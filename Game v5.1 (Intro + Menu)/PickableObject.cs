using Godot;
using System;

public class PickableObject : RigidBody2D
{
	private bool picked = false;

	private string direction;
	
	
	public override void _Ready()
	{
		
	}
	
	// Intéractions : Grap / Throw / Drop ------------------------------------------------------------
	
	private void _input(InputEvent @event)
	{
		var collision = GetNode<CollisionShape2D>("CollisionShape2D");
		
		if (@event is InputEventKey eventKey )
		{
			if (eventKey.Pressed && eventKey.Scancode == (int) KeyList.E )
			{
				var bodies = GetNode<Area2D>("detector").GetOverlappingBodies();
			
				foreach (var b in bodies)
				{
					if (picked == false && b.GetType().FullName == "Player")
					{
						picked = true;
						
					}
				}
			}

			if (eventKey.Pressed && eventKey.Scancode == (int) KeyList.R && picked)
			{
				collision.SetDeferred("disabled", false);
				picked = false;
			}

			if (eventKey.Pressed && eventKey.Scancode == (int) KeyList.F && picked)
			{
				collision.SetDeferred("disabled", false);
				EmitSignal("IsGrapping","1");
				picked = false;
				if (direction == "right")
				{
					ApplyImpulse(new Vector2(), new Vector2(600,-200));
				}
				else
				{
					ApplyImpulse(new Vector2(), new Vector2(-600,-200));
				}
				
				
				
			}
		}
	}
	
	public override void _PhysicsProcess(float delta)
	{
		var collision = GetNode<CollisionShape2D>("CollisionShape2D");
		if (picked)
		{
			collision.SetDeferred("disabled", true);
			EmitSignal("IsGrapping","2");
			if (direction == "right")
			{
				Position = GetParent().GetNode<Position2D>("Player/ObjectGrab").GlobalPosition;
			}
			else
			{
				Position = GetParent().GetNode<Position2D>("Player/ObjectGrab2").GlobalPosition;
			}
			Sleeping = true;
			
		}
		else
		{
			EmitSignal("IsGrapping","3");
			Sleeping = false;
		}
	}

	[Signal]
	public delegate void IsGrapping(string cond);
	
	
	private void _on_Player_WichWay(string way)
	{
		if (way == "left")
		{
			direction = "left";
		}

		if (way == "right")
		{
			direction = "right";
		}
	}



  public override void _Process(float delta)
  {
	  
  }
}


