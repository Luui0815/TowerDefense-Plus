using System;
using System.Collections.Generic;
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
	private static readonly Dictionary<Level, Texture2D> _levelPreviewCache = new Dictionary<Level, Texture2D>();
	private PlayerData _playerData;
	private TextureRect _levelPreviewRect;

    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
		_levelPreviewRect = GetNode<TextureRect>("Panel/LevelPreviewPanel/LevelPreview");
		CreateLevelButtons();
    }

	private void CreateLevelButtons()
	{
		Container levelButtonContainer = GetNode<Container>("Panel/LevelButtonContainer");
        Theme theme = GD.Load<Theme>("res://theme/ButtonTheme.tres");

        foreach (Level level in Enum.GetValues(typeof(Level))) 
		{
			if (!_levelPreviewCache.ContainsKey(level))
			{
				Texture2D previewTexture = GD.Load<Texture2D>($"res://assets/texture/level/LevelPreview{level}.png");
				_levelPreviewCache.Add(level, previewTexture);
			}
			bool levelUnlocked = level == Level.One || _playerData.CompletedLevels.Contains(((int)level) - 1);
            Button button = new()
            {
				CustomMinimumSize = new Vector2(180, 70),
				Disabled = !levelUnlocked,
				Text = $"LEVEL {(int)level}",
				Theme = theme
            };
			button.Pressed += () => OnLevelButtonPressed(level);
			if (!button.Disabled)
			{
				button.MouseEntered += () => OnLevelButtonMouseEntered(level);
				button.MouseExited += OnLevelButtonMouseExited;
			}
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

	private void OnLevelButtonMouseEntered(Level level)
	{
		_levelPreviewRect.Texture = _levelPreviewCache[level];
	}

	private void OnLevelButtonMouseExited()
	{
		_levelPreviewRect.Texture = null;
	}

	private void OnLevelButtonPressed(Level level)
	{
		_playerData.CurrentLevel = level;
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}

	private void OnGoBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
	}
}
