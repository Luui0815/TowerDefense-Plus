extends Control

@export var game_manger : GameManager # exportiert alle functionen und variabeln von GameManger - also die pausiersignale

# Called when the node enters the scene tree for the first time.
func _ready(): # wenn spiel zum ersten mal gestartet
	hide() # versteck das pausemenü
	game_manger.connect("toggle_game_paused", _on_game_manager_toggle_game_paused) # übergibt pausesignal an function


# Called every frame. 'delta' is the elapsed time since the previous frame.
@warning_ignore("unused_parameter")
func _process(delta):
	pass

func _on_game_manager_toggle_game_paused(is_paused : bool):
	if (is_paused): # true fall
		show()
	else: # false fall
		hide()

func _on_exit_level_button_pressed(): # geht zu root Szene von MainMenu über ConfirmationPopup
	get_tree().change_scene_to_file("res://scene/ui/ConfirmationPopup.tscn")


func _on_close_button_pressed(): # geht aus dem aktiven Pausemenü raus, pausiert es, zurück in die pausierte Level Szene, und aktiviert sie wieder
	game_manger.game_paused = false


func _on_option_button_pressed(): # geht zu root Szene von OptionMenu
	get_tree().change_scene_to_file("res://scene/ui/OptionMenu.tscn")
