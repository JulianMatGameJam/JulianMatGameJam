using Godot;
using System;

public class Planche : RigidBody2D
{
	
	private int numbertouch = 0;
	
	public override void _Ready()
	{
		
	}
	private void _on_Platform_HasTouch(int number)
	{
		numbertouch += number;
	}



  public override void _Process(float delta)
  {
	  if (numbertouch == 3)
	  {
		  
		  this.Mode = ModeEnum.Rigid;
	  }
  }
}



