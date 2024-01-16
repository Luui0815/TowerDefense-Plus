using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class TowerSelectionMenu : Node
{
    private PlayerData _playerData;
    //private GridContainer _availableTowersContainer;
    private Panel _availableTowersContainer;
    private Panel _selectedTowersContainer;
    private Button _startLevelButton;
    private Label _availableTowerNumberDisplay;

    private string _levelNumber;
    private int _selectedTowerCount = 0;
    private SortedSet<string> _selectedTowers;

    private Vector2 _Position=Vector2.Zero;
    private List<tower_selection_grid> _SelectedTowersList = new();


    public override void _Ready()
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        _selectedTowers = new SortedSet<string>(new TowerComparer(towerConfig));

        _playerData = GetNode<PlayerData>("/root/PlayerData");
        //_availableTowersContainer = GetNode<GridContainer>("AvailableTowersContainer");
        _availableTowersContainer = GetNode<Panel>("AvailableTowersContainer");
        _selectedTowersContainer = GetNode<Panel>("SelectedTowersContainer");
        _startLevelButton = GetNode<Button>("Panel/StartLevelButton");
        _availableTowerNumberDisplay = GetNode <Label> ("Panel/DisplayAvailableNumber");

        //TODO: Remove hardcoded value
        _levelNumber = "One";
        _startLevelButton.Disabled = true;
        _availableTowerNumberDisplay.Text = "Noch 4 Türme auswählbar";
        _playerData.Load();

        //CreateTowerButtons();
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
            _SelectedTowersList.First(x => x.button.Text == clickedButton.Text).QueueFree();
            _SelectedTowersList.Remove(_SelectedTowersList.First(x=>x.button.Text== clickedButton.Text));
            foreach (string tower in _selectedTowers)
                CreateSelectedTowers(tower);
        }
        else if (_selectedTowerCount < 4)
        {
            //hier aufgerufen
            _selectedTowers.Add(clickedButton.Text);
            _selectedTowerCount++;
            //Label towerLabel = new();
            //towerLabel.Text = towerLabel.Name = clickedButton.Text;
            //_selectedTowersContainer.AddChild(towerLabel);
            CreateSelectedTowers(clickedButton.Text);
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
        _Position = Vector2.Zero;
        foreach (string tower in _playerData.UnlockedTowers)
        {
            tower_selection_grid TowerContainer= CreateTowers(_availableTowersContainer, tower, _playerData.UnlockedTowers.Count);
            TowerContainer.button.Pressed += () => OnTowerButtonPressed(TowerContainer.button);
        }
    }

    private void CreateSelectedTowers(string newTower)
    {
        //alle alten Tuerme loeschen
        foreach (tower_selection_grid tower in _SelectedTowersList)
            tower.QueueFree();
        _SelectedTowersList.Clear();

        _Position = Vector2.Zero;
        //alle Tuerme neu positionieren
        if(_selectedTowerCount!=0)
        {
            foreach (string tower in _selectedTowers)
            {
                _SelectedTowersList.Add(CreateTowers(_selectedTowersContainer, tower, _selectedTowerCount));
            }
        }

    }

    private tower_selection_grid CreateTowers(Panel panel,string tower, int ElementCount)
    {
        Vector2 Gap;
        tower_selection_grid TowerSelection = (tower_selection_grid)GD.Load<PackedScene>("res://scene/ui/tower_selection_grid.tscn").Instantiate();
        TowerSelection.Init(_selectedTowers.Contains(tower), tower);
        //Abstand in X-Richtung zwischen den Tuermen berechnen
        Gap.X = (panel.Size.X - 40 - (100 * ElementCount)) / (ElementCount + 1);
        //Abstnd oben und unten ermitteln
        Gap.Y = (panel.Size.Y - 125) / 2;
        TowerSelection.Position = _Position + Gap;
        panel.AddChild(TowerSelection);
        _Position.X += Gap.X + 100;

        return TowerSelection;
    }

    private void CheckStartLevelButton()
    {
        _startLevelButton.Disabled = _selectedTowerCount == 0 || _selectedTowerCount > 4;
    }
}
