using System;
using Godot;
using TowerDefense;

namespace TowerDefense {
	public enum Level {
		One = 1,
		Two = 2
	}
}

public partial class LevelSelectionMenu : Node
{
	private PlayerData _playerData;

    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
		CreateLevelButtons();
    }

	private void CreateLevelButtons()
	{
		Container levelButtonContainer = GetNode<Container>("Panel/LevelButtonContainer");
		StyleBoxTexture normalButtonStyleBox = CreateButtonStyleBox("Stonebutton1.png");
		StyleBoxTexture pressedButtonStyleBox = CreateButtonStyleBox("StonebuttonPressed.png");

		Font buttonFont = GD.Load<Font>("res://assets/font/HyliaMidevilFont.otf");
        Theme theme = new()
        {
            DefaultFont = buttonFont
        };

        foreach (Level level in Enum.GetValues(typeof(Level))) 
		{
            Button button = new()
            {
				CustomMinimumSize = new Vector2(180, 70),
				Text = $"LEVEL {(int)level}",
				Theme = theme
            };
            button.AddThemeStyleboxOverride("normal", normalButtonStyleBox);
			button.AddThemeStyleboxOverride("pressed", pressedButtonStyleBox);
			button.Pressed += () => OnLevelButtonPressed(level);
			levelButtonContainer.AddChild(button);
		}
	}

	private StyleBoxTexture CreateButtonStyleBox(string textureName)
	{
		Texture2D buttonTexture = GD.Load<Texture2D>($"res://assets/button/{textureName}");
        StyleBoxTexture styleBoxTexture = new()
        {
            Texture = buttonTexture,
            ExpandMarginLeft = 7,
            ExpandMarginRight = 7
        };
		return styleBoxTexture;
    }

    public void OnSettingsButtonPressed()
    {
        OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
        AddChild(optionMenu);
    }

	public void OnGoBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
	}

	public void OnLevelButtonPressed(Level level)
	{
		_playerData.CurrentLevel = level;
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}
}
