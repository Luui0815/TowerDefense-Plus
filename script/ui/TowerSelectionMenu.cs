using Godot;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

public partial class TowerSelectionMenu : Node
{
    private PlayerData _playerData;
    private Panel _availableTowersContainer;
    private Panel _selectedTowersContainer;
    private Button _startLevelButton;
    private Label _availableTowerNumberDisplay;

    private Level _levelNumber;
    private int _selectedTowerCount = 0;
    private SortedSet<string> _availableTowers;
    private SortedSet<string> _selectedTowers;

    private Vector2 _position=Vector2.Zero;
    private List<TowerSelectionGridItem> _selectedTowersList = new();


    public override void _Ready()
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        _selectedTowers = new SortedSet<string>(new TowerComparer(towerConfig));
        _availableTowers = new SortedSet<string>(new TowerComparer(towerConfig));

        _playerData = GetNode<PlayerData>("/root/PlayerData");
        _availableTowersContainer = GetNode<Panel>("AvailableTowersContainer");
        _selectedTowersContainer = GetNode<Panel>("SelectedTowersContainer");
        _startLevelButton = GetNode<Button>("Panel/StartLevelButton");
        _availableTowerNumberDisplay = GetNode <Label> ("Panel/DisplayAvailableNumber");

        _levelNumber = _playerData.CurrentLevel;
        _startLevelButton.Disabled = true;
        _availableTowerNumberDisplay.Text = "Noch 4 Türme auswählbar";
        _playerData.Load();

        Label headlineLabel = GetNode<Label>("Panel/MainHeadline");
        headlineLabel.Text = $"LEVEL {(int)_levelNumber} - TURMAUSWAHL";

        CreateAvailableTowers();
    }

    private void OnGoBackButtonPressed()
    {
        ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
        confirmationPopup.Init("Willst du wirklich zur Levelauswahl zurückgehen?", "Zur Levelauswahl zurückgehen");
        confirmationPopup.Confirmed += () => GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
        AddChild(confirmationPopup);
    }

    private void OnTowerButtonPressed(Button clickedButton)
    { 
        CheckStartLevelButton();

        if (_selectedTowers.Contains(clickedButton.Text))
        {
            _selectedTowerCount--;
            _selectedTowers.Remove(clickedButton.Text);
            _selectedTowersList.First(x => x.TowerButton.Text == clickedButton.Text).QueueFree();
            _selectedTowersList.Remove(_selectedTowersList.First(x=>x.TowerButton.Text== clickedButton.Text));
            foreach (string tower in _selectedTowers)
                CreateSelectedTowers();
        }
        else if (_selectedTowerCount < 4)
        {
            //hier aufgerufen
            _selectedTowers.Add(clickedButton.Text);
            _selectedTowerCount++;
            CreateSelectedTowers();
        }

        int towerLeftCount = 4 - _selectedTowerCount;
        string towerText = towerLeftCount == 1 ? "Turm" : "Türme";
        _availableTowerNumberDisplay.Text = towerLeftCount > 0 ? $"Noch {towerLeftCount} {towerText} auswählbar" : "Kein Turm mehr auswählbar";

        CheckStartLevelButton();
    }

    private void OnStartLevelButtonPressed()
    {
        PackedScene levelScene = GD.Load<PackedScene>($"res://scene/map/level/Level{_levelNumber}.tscn");
        GameLevel gameLevel = (GameLevel)levelScene.Instantiate();
        GetTree().Root.AddChild(gameLevel);
        gameLevel.FillTowerContainer(_selectedTowers);
        QueueFree();
    }

    private void CreateAvailableTowers()
    {
        _position = Vector2.Zero;
        foreach (string tower in _playerData.UnlockedTowers)
        {
            _availableTowers.Add(tower);
        }
        foreach (string tower in _availableTowers)
        {
            TowerSelectionGridItem TowerContainer= CreateTowers(_availableTowersContainer, tower, _playerData.UnlockedTowers.Count);
            TowerContainer.TowerButton.Pressed += () => OnTowerButtonPressed(TowerContainer.TowerButton);
        }
    }

    private void CreateSelectedTowers()
    {
        //alle alten Tuerme loeschen
        foreach (TowerSelectionGridItem tower in _selectedTowersList)
        {
            tower.QueueFree();
        }
        _selectedTowersList.Clear();

        _position = Vector2.Zero;
        //alle Tuerme neu positionieren
        if(_selectedTowerCount!=0)
        {
            foreach (string tower in _selectedTowers)
            {
                _selectedTowersList.Add(CreateTowers(_selectedTowersContainer, tower, _selectedTowerCount));
            }
        }

    }

    private TowerSelectionGridItem CreateTowers(Panel panel,string tower, int ElementCount)
    {
        Vector2 Gap;
        TowerSelectionGridItem TowerSelection = (TowerSelectionGridItem)GD.Load<PackedScene>("res://scene/ui/TowerSelectionGridItem.tscn").Instantiate();
        TowerSelection.Init(_selectedTowers.Contains(tower), tower);
        //Abstand in X-Richtung zwischen den Tuermen berechnen
        Gap.X = (panel.Size.X - 40 - (100 * ElementCount)) / (ElementCount + 1);
        //Abstnd oben und unten ermitteln
        Gap.Y = (panel.Size.Y - 125) / 2;
        TowerSelection.Position = _position + Gap;
        panel.AddChild(TowerSelection);
        _position.X += Gap.X + 100;

        return TowerSelection;
    }

    private void CheckStartLevelButton()
    {
        _startLevelButton.Disabled = _selectedTowerCount == 0 || _selectedTowerCount > 4;
    }
}
