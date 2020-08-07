using Godot;
using System;

public class Main : Node
{

	private AudioStreamPlayer windsound;
	private AudioStreamPlayer stepsound;

	

	public override void _Ready()
	{
		windsound = GetNode<AudioStreamPlayer>("Music/WindSound");
		stepsound = GetNode<AudioStreamPlayer>("Music/StepSound");
		windsound.Play();
	}

	

	public override void _Process(float delta)
  {
	  
 }
}



