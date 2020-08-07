using Godot;
using System;

public class ActivableTrap : RigidBody2D
{
	private Sprite enablesprite;
	private Sprite disablesprite;
	
	
	public override void _Ready()
	{
		 enablesprite = GetNode<Sprite>("EnableSprite");
		 disablesprite = GetNode<Sprite>("DisableSprite");
		
		enablesprite.Hide();
	}
	
	private void _on_ActivableTrap_body_entered(object body)
	{
		GD.Print("lol");
	}
	
	
	private void _on_Area2D_area_entered(object area)
	{
		enablesprite.Show();
		disablesprite.Hide();
		EmitSignal("ButtonUp",true);
	}
	
	private void _on_Area2D_area_exited(object area)
	{
		enablesprite.Hide();
		disablesprite.Show();
		EmitSignal("ButtonUp",false);
	}
	
	[Signal]
	public delegate void ButtonUp(bool cond);
	
	
	
 public override void _Process(float delta)
  {
	
 }
}











