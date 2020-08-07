using Godot;
using System;

public class Elevator : RigidBody2D
{

	private string stringcond = "";
	
	public override void _Ready()
	{
		
	}
	
	private void _on_ActivableTrap3_ButtonUp(bool cond)
	{
		if (cond)
		{
			stringcond += "1";
		}
	}


	private void _on_ActivableTrap2_ButtonUp(bool cond)
	{
		if (cond)
		{
			stringcond += "2";
		}
	}


	private void _on_ActivableTrap_ButtonUp(bool cond)
	{
		if (cond)
		{
			stringcond += "3";
		}
		
	}

	


	public override void _PhysicsProcess(float delta)
	{
		if (stringcond == "123")
		{
			GD.Print("ca marche");
			var position = Position;
			position.y -= 1;
		}
	}
}



