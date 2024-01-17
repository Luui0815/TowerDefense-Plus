using Godot;
using System;

public partial class GameTutorial : Control
{
	private string[] _tutorialText = 
		{"Willkommen beim BA Tower Defense Game!\nDiese kleine Einfuehrung soll dem Spieler die grundlegenden Prinzipien, Inhalte und Ziele des Spiels vermitteln.",
         "Tower Defense ist ein Genre von Strategiespielen, in welchem der Spieler eine Verteidigungslinie aufbauen muss, um Wellen von ankommenden Feinden abzuwehren. Die verteidigenden Einheiten werden dabei Tuerme (englisch: towers) genannt.",
         "In diesem Spiel kann der Spieler zwischen mehreren Leveln auswaehlen. Sie unterscheiden sich primaer in ihrem Schwierigkeitsgrad. Ziel eines Levels ist, alle Gegnerwellen zu ueberstehen, ohne dass eine gegnerische Einheit das Schloss auf der linken Seite des Bildschirms erreicht.",
		 "Wurde ein Level ausgewaehlt, hat der Spieler eine Auswahl von verschiedenen Einheiten, welche er im Level verwenden kann. Als naechstes werden die wichtigsten Konzepte und Einheitentypen des Spiels erklaert.",
         "1. Geld generieren\nEinheiten in einem Level zu platzieren, kostet je nach Einheit eine Summe an Geld. Diese muss der Spieler primaer durch das Platzieren von Goldminen verdienen. Diese generieren in einem bestimmten Intervall Geld.",
         "Jedoch lassen auch besiegte Gegner eine kleinere Menge Geld fallen.\n4-5 Goldminen in einem Level zu platzieren wird empfohlen.",
		 "2. Verteidiger platzieren und Gegner abwehren\n Man kann zwischen zwei Kategorien von Verteidigern unterscheiden: Aktive Verteidiger und passive Verteidiger.\n Aktive Verteidiger greifen aktiv Feinde mit Geschossen oder im Nahkampf an, wenn diese sich in Reichweite befinden.\n Passive Verteidiger können Gegner aufhalten oder verlangsamen und die aktiven Verteidiger unterstützen.",
		 "" };
	private int _tutorialTextIndex = 0;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void OnGoBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
    }
}
