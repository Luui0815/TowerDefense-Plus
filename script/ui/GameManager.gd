extends Node

class_name GameManager # Classe/ Instanz zum Steuern des Pausiersignals an alle Untermenüs und Elemente

signal toggle_game_paused(is_paused : bool) #signal an functionen was ansagt ob szenen pausiert werden sollen oder nicht

var game_paused : bool = false: # variable die ansagt ob Szene pasusiert oder nicht
	get: # nimmt sich aktuellen Wert
		return game_paused
	set(value): # verändert variablenwert abhängig von value (Eingabe User)
		# hier muss sich dann der neue Input/ Trigger eingbeunden werden, der von einer Level UI Szene bzw der Map Szene erzeugt wird 
		game_paused = value
		#die get_tree() Methode zeigt übergreifend auf den GameManger
		get_tree().paused = game_paused # damit die Szene des Levels pausiert ist muss wurzel ihren prozesswert ändern auf pausiert
		emit_signal("toggle_game_paused", game_paused) # erzeugen des Signals zum pausieren und übergabe an variable game_paused

func _input(event : InputEvent): # wenn ESC gedrückt wird (ESC = ui_cancel)
	if (event.is_action_pressed("ui_cancel")):
		game_paused = not game_paused #invertiert das aktuelle signal/ Zustand Szene (pausiert/ nicht pausiert)
