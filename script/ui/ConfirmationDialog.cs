using Godot;
using System;

public partial class ConfirmationDialog : Window
{
	private bool _cancelButtonPressed = false;
	private bool _confirmButtonPressed = false;

	public void Init(string message, string boxTitle)
	{
		//Titel der MsgBox festlegen
		Title = boxTitle;

		//angezeigten Text festlegen
		Label textLabel = GetNode<Label>("TextLabel");
		textLabel.Text = message;
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
		Hide();
	}

	private void OnConfirmButtonPressed()
	{
		_confirmButtonPressed=true;
		Hide();
	}

	private void OnCloseRequested()
	{
		OnCancelButtonPressed();
	}

}
