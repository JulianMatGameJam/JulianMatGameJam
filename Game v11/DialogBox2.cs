using Godot;
using System;

public class DialogBox2 : Polygon2D
{
	private int page = 0;
	private RichTextLabel text;

	private string[] dialog = new[]
	{
		"Vas-y prête moi un peu de ton énergie et libérons mes enfants",
		"Merci beaucoup pour ton aide, je ne l'oublierai pas", "Au revoir"
	};

	
	private bool gameend = false;
	
	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("RichTextLabel");
		text.SetBbcode(dialog[page]);
		text.SetVisibleCharacters(0);
		SetProcessInput(true);
	}
	
	private void _input(InputEvent @event)
	{
		if (gameend)
		{
			if (@event is InputEventKey eventKey && page <= 3)
			{
				if (text.GetVisibleCharacters() > text.GetTotalCharacterCount())
				{
					if (page < dialog.Length -1)
					{
						page += 1;
						text.SetBbcode(dialog[page]);
						text.SetVisibleCharacters(0);
					}
					else
					{
						GetTree().ChangeScene("res://EndGame.tscn");
						
					}
				}
				/*else
				{
					text.SetVisibleCharacters(text.GetTotalCharacterCount());
				}*/
			}
		}
		

		
	}
	
	private void _on_Timer_timeout()
	{
		if (gameend)
		{
			text.SetVisibleCharacters(text.GetVisibleCharacters() +1);
		}
		
	}
	
	private void _on_Player_GameIsFinish(bool cond)
	{
		gameend = cond;
	}
	
	


  public override void _Process(float delta)
  {
	  
  }
}






