using Godot;
using System;

public partial class GameTutorial : Control
{
    private string[] _tutorialText =
        {"Willkommen beim BA Tower Defense Game!\nDiese kleine Einführung soll dem Spieler die grundlegenden Prinzipien, Inhalte und Ziele des Spiels vermitteln.",
         "Tower Defense ist ein Genre von Strategiespielen, in welchem der Spieler eine Verteidigungslinie aufbauen muss, um Wellen von ankommenden Feinden abzuwehren. Die verteidigenden Einheiten werden dabei Türme (englisch: towers) genannt.",
         "In diesem Spiel kann der Spieler zwischen mehreren Leveln auswählen. Sie unterscheiden sich primaer in ihrem Schwierigkeitsgrad. Ziel eines Levels ist, alle Gegnerwellen zu überstehen, ohne dass eine gegnerische Einheit das Schloss auf der linken Seite des Bildschirms erreicht.",
         "Wurde ein Level ausgewählt, hat der Spieler eine Auswahl von verschiedenen Einheiten, welche er im Level verwenden kann. Als nächstes werden die wichtigsten Konzepte und Einheitentypen des Spiels erklärt.",
         "1. Geld generieren\nEinheiten in einem Level zu platzieren, kostet je nach Einheit eine Summe an Geld. Diese muss der Spieler primär durch das Platzieren von Goldminen verdienen. Diese generieren in einem bestimmten Intervall Geld.",
         "Jedoch lassen auch besiegte Gegner eine kleinere Menge Geld fallen.\n4-5 Goldminen in einem Level zu platzieren wird empfohlen.",
         "2. Verteidiger platzieren und Gegner abwehren\n Man kann zwischen zwei Kategorien von Verteidigern unterscheiden: Aktive Verteidiger und passive Verteidiger.",
         "Aktive Verteidiger greifen aktiv Feinde mit Geschossen oder im Nahkampf an, wenn diese sich in Reichweite befinden.",
         "Passive Verteidiger können Gegner aufhalten oder verlangsamen und die aktiven Verteidiger unterstützen." };

    private int _tutorialTextIndex = 0;
    private Label _infoText, _pageLabel;
    private TextureRect _imagePanel;
    private Button _showNextButton, _showPreviousButton;
    public override void _Ready()
    {
        _infoText = GetNode<Label>("InfoTextLabel");
        _imagePanel = GetNode<TextureRect>("InfoImage");
        _showPreviousButton = GetNode<Button>("ShowPreviousButton");
        _showNextButton = GetNode<Button>("ShowNextButton");
        _pageLabel = GetNode<Label>("PageLabel");

        UpdateUserInterface();
    }

    private void OnMainMenuButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
    }

    private void OnShowNextButtonPressed()
    {
        if(_tutorialTextIndex < _tutorialText.Length)
        {
            _tutorialTextIndex++;
            UpdateUserInterface();
        }
    }

    private void OnShowPreviousButtonPressed()
    {
        if(_tutorialTextIndex > 0) 
        {
            _tutorialTextIndex--;
            UpdateUserInterface();
        }
    }

    private void UpdateUserInterface()
    {
        _showPreviousButton.Disabled = _tutorialTextIndex == 0;
        _showNextButton.Disabled = _tutorialTextIndex == (_tutorialText.Length - 1);

        _infoText.Text = _tutorialText[_tutorialTextIndex];
        _pageLabel.Text = $"{_tutorialTextIndex + 1}/{_tutorialText.Length}";
        //TODO: Load image
    }
}
