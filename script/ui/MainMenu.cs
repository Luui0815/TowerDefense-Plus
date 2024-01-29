using Godot;

public partial class MainMenu : Node
{
	private const string _creditFilePath = "res://config/credits.txt";
	private CanvasLayer _creditCanvas;

    public override void _Ready()
    {
        //Mousecursor anpassen
        Resource arrow = ResourceLoader.Load("res://assets/texture/mousecursor/MauscourserV1.png");
        Input.SetCustomMouseCursor(arrow, Input.CursorShape.Drag);
        Input.SetCustomMouseCursor(arrow, Input.CursorShape.CanDrop);
		Input.SetCustomMouseCursor(arrow, Input.CursorShape.PointingHand);

		_creditCanvas = GetNode<CanvasLayer>("CreditCanvas");
		LoadCreditText();
    }

	private void LoadCreditText()
	{
		if (!FileAccess.FileExists(_creditFilePath))
		{
			return;
		}

		RichTextLabel creditTextLabel = GetNode<RichTextLabel>("CreditCanvas/CreditBackground/CreditText");
		using FileAccess creditFile = FileAccess.Open(_creditFilePath, FileAccess.ModeFlags.Read);
		if (creditFile == null)
		{
			GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
		}

		string text = creditFile.GetAsText();
		creditTextLabel.Text = text;
	}

	private void OnExitButtonPressed()
	{
		ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		confirmationPopup.Init("Willst du wirklich das Spiel verlassen?", "Spiel verlassen");
		confirmationPopup.Confirmed += () => GetTree().Quit();
		AddChild(confirmationPopup);
	}

	public void OnSettingsButtonPressed()
	{
		OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(optionMenu);
	}

	public void OnLevelSelectionButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
	}

    public void OnAboutProjectButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/ui/GameTutorial.tscn");
    }

	public void OnCreditsButtonPressed()
	{
		_creditCanvas.Visible = true;
	}

	public void OnCloseCreditsButtonPressed()
	{
		_creditCanvas.Visible = false;
	}
}

