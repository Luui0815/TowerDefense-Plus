using Godot;

public partial class OptionMenu : Window
{
	private PlayerData _playerData;

	public override void _Ready()
	{
		_playerData = GetNode<PlayerData>("/root/PlayerData");
		_playerData.Load();
	}

	private void OnResetButtonPressed()
	{
		ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		confirmationPopup.Init("Möchtest du wirklich deinen Fortschritt zurücksetzen? Dieser Schritt kann nicht rückgängig gemacht werden", "Fortschritt zurücksetzen");
		confirmationPopup.Confirmed += () =>  {
			_playerData.ResetTowers();
			_playerData.ResetCompletedLevels();
			_playerData.Save();
			QueueFree();
		};
		AddChild(confirmationPopup);
	}

	private void OnCloseRequested()
	{
		QueueFree();
	}

	private void OnCloseButtonPressed()
	{
		OnCloseRequested();
	}

}


