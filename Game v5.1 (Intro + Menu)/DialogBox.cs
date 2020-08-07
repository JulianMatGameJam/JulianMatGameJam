using Godot;
using System;

public class DialogBox : Polygon2D
{
	private string[] dialog = new[]
	{
		"Hey toi ! J'ai besoin de ton aide.",
		"Ma famille s'est fait capturer par une araignée géante et je n'ai pas assez de force pour les libérer de moi-même.",
		"ATTENTION! Elle arrive derrière toi. Rejoins moi vite! Avant que mes enfants ne deviennent son prochain repas."
	};
	private int page = 0;
	private RichTextLabel text;
	
	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("RichTextLabel");
		text.SetBbcode(dialog[page]);
		text.SetVisibleCharacters(0);
		SetProcessInput(true);
	}

	private void _input(InputEvent @event)
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
					this.Hide();
					EmitSignal("GameStart",true);
				}
			}
			/*else
			{
				text.SetVisibleCharacters(text.GetTotalCharacterCount());
			}*/
		}
	}
	
	[Signal]
	private delegate void GameStart(bool cond);
	
	private void _on_Timer_timeout()
	{
		text.SetVisibleCharacters(text.GetVisibleCharacters() +1);
	}

	


  public override void _Process(float delta)
  {
	  
 }
}



