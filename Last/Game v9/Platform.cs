using Godot;
using System;

public class Platform : RigidBody2D
{
	private Sprite toilesprite;
	
	public override void _Ready()
	{
		toilesprite = GetNode<Sprite>("Sprite2");
	}
	
	private void _on_detector_area_entered(object area)
	{
		toilesprite.Hide();
		EmitSignal("HasTouch",1);
	}

	[Signal]
	private delegate void HasTouch(int number);


  public override void _Process(float delta)
  {
	  
 }
}



