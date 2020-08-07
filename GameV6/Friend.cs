using Godot;
using System;

public class Friend : AnimatedSprite
{
	private AnimatedSprite friend;
	
	public override void _Ready()
	{
		
		this.Animation = "Idle";
		this.FlipH = true;
		this.Play();
	}
	
	private void _on_DialogBox_GameStart(bool cond)
	{
		if (cond)
		{
			this.SpeedScale = 2;
			this.Animation = "Disparition";
			this.FlipH = true;
			this.Play();
		}
	}
	
	private void _on_Friend_animation_finished()
	{
		if (this.Animation == "Disparition")
		{
			this.Hide();
		}
		
	}

	

  public override void _Process(float delta)
  {
	  
  }
}







