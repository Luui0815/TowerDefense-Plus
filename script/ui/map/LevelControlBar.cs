using Godot;
using System;

public partial class LevelControlBar : Control
{
	private Label _moneyLabel;
	private Button _startButton;

	[Signal]
	public delegate void StartButtonPressedEventHandler(Button startButton);

	[Signal]
	public delegate void PauseButtonPressedEventHandler();

    public override void _Ready()
    {
        _moneyLabel = GetNode<Label>("ControlBar/VBoxContainer/MoneyContainer/CenterContainer/HBoxContainer/MoneyLabel");
		_startButton = GetNode<Button>("ControlBar/VBoxContainer/ButtonContainer/HBoxContainer/StartButton");
    }

    public void DisplayMoney(int money)
	{
		_moneyLabel.Text = Convert.ToString(money);
	}

	public void OnStartButtonPressed()
	{
		EmitSignal(SignalName.StartButtonPressed, _startButton);
	}

	public void OnPauseButtonPressed()
	{
		EmitSignal(SignalName.PauseButtonPressed);
	}
}
