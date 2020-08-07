using Godot;
using System;

public class Elevator : RigidBody2D
{

	private bool cond1 = false;
	private bool cond2 = false;
	private bool cond3 = false;
	private Sprite sprite;
	private CollisionShape2D collision;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite>("Sprite");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
	}
	
	private void _on_ActivableTrap3_ButtonUp(bool cond)
	{
		if (cond)
		{
			cond1 = true;
		}
	}


	private void _on_ActivableTrap2_ButtonUp(bool cond)
	{
		if (cond)
		{
			cond2 = true;
		}
	}


	private void _on_ActivableTrap_ButtonUp(bool cond)
	{
		if (cond)
		{
			cond3 = true;
		}
		
	}

	


	public override void _PhysicsProcess(float delta)
	{
		
		if (cond1 && cond2 && cond3)
		{
			this.QueueFree();
		}
	}
}



