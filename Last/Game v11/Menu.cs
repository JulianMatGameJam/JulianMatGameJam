using Godot;
using System;

public class Menu : CanvasLayer
{
	private Label labelmessage;
	private TextureButton startbutton;

	public override void _Ready()
	{
		labelmessage = GetNode<Label>("Label");
		startbutton = GetNode<TextureButton>("TextureButton");
	}
	
	private void _on_TextureButton_pressed()
	{
		labelmessage.Hide();
		startbutton.Hide();
		GetTree().ChangeScene("res://Main.tscn");
	}


  public override void _Process(float delta)
  {
	 
 }
}



