using Godot;
using System;

public partial class ConfirmationDialog : Window
{
	private bool _cancelButtonPressed;
	private bool _confirmButtonPressed;
	private bool _interactionFinished;//sagt aus ob der User ueberhupt was gedrueckt hat
	private Window _confirmationDialogWindow;
	private Label _textLabel;
	private Button _cancelButton;
	private Button _confirmButton;

	
	public void Init(string message, string boxTitle)
	{
		//Titel der MsgBox festlegen
		Title = boxTitle;
		//angezeigten Text festlegen
		_textLabel = GetNode<Label>("TextLabel");
		_textLabel.Text = message;

		//Instanzen der Buttons festlegen
		_cancelButton = GetNode<Button>("CancelButton");
		_confirmButton = GetNode<Button>("ConfirmButton");

		//restliche globale Variablen festlegen
		_cancelButtonPressed = false;
		_confirmButtonPressed = false;
		_interactionFinished = false;
	}

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public bool Cancelled
	{
		get 
		{ 
			return _cancelButtonPressed; 
		}
	}

	public bool Confirmed
	{
		get
		{
			return _confirmButtonPressed;
		}
	}

	private void OnCancelButtonPressed()
	{
		_cancelButtonPressed = true;
		_interactionFinished = true;
		Hide();
	}

	private void OnConfirmButtonPressed()
	{
		_confirmButtonPressed=true;
		_interactionFinished=true;
		Hide();
	}

	private void OnCloseRequested()
	{
		OnCancelButtonPressed();
	}

}
