using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class TowerSelectionMenu : Node
{
    private PlayerData _playerData;
    private GridContainer _availableTowersContainer;
    private GridContainer _selectedTowersContainer;
    private Button _startLevelButton;
    private Label _availableTowerNumberDisplay;

    private int _levelNumber;
    private int _selectedTowerCount = 0;
    private List<string> _selectedTowers = new();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
        _availableTowersContainer = GetNode<GridContainer>("AvailableTowersContainer");
        _selectedTowersContainer = GetNode<GridContainer>("SelectedTowersContainer");
        _startLevelButton = GetNode<Button>("StartLevelButton");
        _availableTowerNumberDisplay = GetNode <Label> ("DisplayAvailableNumber");

        _levelNumber = 1;
        _startLevelButton.Disabled = true;
        _availableTowerNumberDisplay.Text = "Noch " + (4 - _selectedTowerCount) + " Tuerme auswaehlbar.";
        _playerData.Load();

        CreateTowerButtons();
    }

    private void OnGoBackButtonPressed()
    {
        ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
        confirmationPopup.Init("Willst du wirklich zur Levelauswahl zurueckgehen?", "Zur Levelauswahl zurueckgehen");
        confirmationPopup.Confirmed += () => GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
        AddChild(confirmationPopup);
    }

    private void OnTowerButtonPressed(Button clickedButton)
    { 
        CheckStartLevelButton();

        if (!_selectedTowers.Contains(clickedButton.Text) && (_selectedTowerCount < 4))
        {
            _selectedTowers.Add(clickedButton.Text);
            _selectedTowerCount++;
            Label towerLabel = new();
            towerLabel.Text = towerLabel.Name = clickedButton.Text;
            _selectedTowersContainer.AddChild(towerLabel);
            _availableTowerNumberDisplay.Text = "Noch " + (4 - _selectedTowerCount) + " Tuerme auswaehlbar.";
        }
        else
        {
            _selectedTowers.Remove(clickedButton.Text);
            _selectedTowerCount--;
            _selectedTowersContainer.GetNode(clickedButton.Text).QueueFree();
            _availableTowerNumberDisplay.Text = "Noch " + (4 - _selectedTowerCount) + " Tuerme auswaehlbar.";
        }

        CheckStartLevelButton();
    }

    private void OnStartLevelButtonPressed()
    {
        //PackedScene gameLevel = GD.Load<PackedScene>("");
        //GameLevel instance = (GameLevel)gameLevel.Instantiate();
        //instance.FillTowerContainer(_selectedTowers);
        //GetTree().ChangeSceneToFile();
    }

    private void CreateTowerButtons()
    {
        foreach (string tower in _playerData.UnlockedTowers)
        {
            Button towerButton = new();
            towerButton.Text = towerButton.Name = tower;
            towerButton.Pressed += () => OnTowerButtonPressed(towerButton);
            _availableTowersContainer.AddChild(towerButton);
        }
    }

    private void CheckStartLevelButton()
    {
        _startLevelButton.Disabled = _selectedTowerCount != 4;
    }
}
