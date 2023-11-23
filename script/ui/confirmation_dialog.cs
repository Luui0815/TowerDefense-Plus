using Godot;
using System;

public partial class confirmation_dialog : Window
{
	private bool Abbrechen_Button_gedrueckt;
    private bool Bestaetigen_Button_gedrueckt;
	private bool interaction_finished;//sagt aus ob der User ueberhupt was gedrueckt hat
	private Window confirmation_dialog_window;
	private Label Text_Label;
	private Button Abbrechen_Button;
	private Button Bestaetigen_Button;

	
	public void _init(string message, string box_title)
	{
		//Titel der MsgBox festlegen
		confirmation_dialog_window = GetNode<Window>("confirmation_dialog");
		confirmation_dialog_window.Title = box_title;
		//angezeigten Text festlegen
		Text_Label = GetNode<Label>("Text_Label");
		Text_Label.Text = message;

		//Instanzen der Buttons festlegen
		Abbrechen_Button = GetNode<Button>("Abbrechen_Button");
        Bestaetigen_Button = GetNode<Button>("Bestaetigen_Button");

		//restliche globale Variablen festlegen
		Abbrechen_Button_gedrueckt = false;
		Bestaetigen_Button_gedrueckt = false;
		interaction_finished = false;
    }

    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public bool canceled
	{
		get 
		{ 
			return Abbrechen_Button_gedrueckt; 
		}
	}

	public bool confirmed
	{
		get
		{
			return Bestaetigen_Button_gedrueckt;
		}
	}

    private void _on_abbrechen_button_pressed()
	{
		Abbrechen_Button_gedrueckt = true;
		interaction_finished = true;
		Hide();
	}

	private void _on_bestaetigen_button_pressed()
	{
		Bestaetigen_Button_gedrueckt=true;
		interaction_finished=true;
		Hide();
	}

    private void _on_close_requested()
	{
		_on_abbrechen_button_pressed();
	}

}
