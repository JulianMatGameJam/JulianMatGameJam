using Godot;
using System;

public class PickableObject : RigidBody2D
{
	private bool picked = false;

	private string direction;
	
	
	public override void _Ready()
	{
		
	}
	
	private void _input(InputEvent @event)
	{
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
				picked = false;
			}

			if (eventKey.Pressed && eventKey.Scancode == (int) KeyList.F && picked)
			{
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
		if (picked)
		{
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


