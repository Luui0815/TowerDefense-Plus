using Godot;
using Godot.Collections;
using System;

public partial class TowerSelectionMenu : Node
{
    private PlayerData _playerData;
    private GridContainer _availableTowersContainer;
    private GridContainer _selectedTowersContainer;
    private Button _startLevelButton;

    private int _levelNumber;
    private int _selectedTowerCount;
    private Array<string> _selectedTowers = new();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
        _availableTowersContainer = GetNode<GridContainer>("AvailableTowersContainer");
        _selectedTowersContainer = GetNode<GridContainer>("SelectedTowersContainer");
        _startLevelButton = GetNode<Button>("StartLevelButton");

        _levelNumber = 1;
        _selectedTowerCount = 0;
        _playerData.Load();

        CreateTowerButtons();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        CheckStartLevelButton();
    }

    private void OnGoBackButtonPressed()
    {
        ConfirmationPopup _confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
        _confirmationPopup.Init("Willst du wirklich zur Levelauswahl zurueckgehen?", "Zur Levelauswahl zurueckgehen");
        _confirmationPopup.Confirmed += () => GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
        AddChild(_confirmationPopup);
    }

    private void OnTowerButtonPressed()
    {
        Button pressedTowerButton = GetNode<Button>("towerButton"); //???

        if (!_selectedTowers.Contains(pressedTowerButton.Text) && (_selectedTowerCount < 4))
        {
            _selectedTowers.Add(pressedTowerButton.Text);
            _selectedTowerCount++;
        }
        else
        {
            _selectedTowers.Remove(pressedTowerButton.Text);
            _selectedTowerCount--;
        }
    }

    private void OnStartLevelButtonPressed()
    {
        //starte level, übergebe array mit ausgewählten türmen
    }

    private void CreateTowerButtons()
    {
        foreach (string tower in _playerData.UnlockedTowers)
        {
            Button towerButton = new();
            towerButton.Text = tower;
            towerButton.Pressed += OnTowerButtonPressed;
            _availableTowersContainer.AddChild(towerButton);
        }
    }

    private void CheckStartLevelButton()
    {
        if (_selectedTowerCount == 4)
        {
            _startLevelButton.Disabled = false;
        }
        else
        {
            _startLevelButton.Disabled = true;
        }
    }
}
