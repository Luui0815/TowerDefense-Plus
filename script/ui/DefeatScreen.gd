extends Node

# Erstelle Zugriffsreferenzen auf alle Animationsplayer der 5 Figuren
@onready var knight : AnimationPlayer = $"Panel/FallenKnight/AnimationPlayer"
@onready var archer : AnimationPlayer = $"Panel/FallenArcher/AnimationPlayer"
@onready var wizard : AnimationPlayer = $"Panel/VictoriousWizard/AnimationPlayer"
@onready var skeleton1 : AnimationPlayer = $"Panel/VictoriousSkeleton1/AnimationPlayer"
@onready var skeleton2 : AnimationPlayer = $"Panel/VictoriousSkeleton2/AnimationPlayer"
@onready var timer_Knight : Timer = $"Panel/FallenKnight/TimerDeathKnight"
@onready var timer_archer : Timer = $"Panel/FallenArcher/TimerDeathArcher"

# Setze die Wartezeiten für die Timer als Konstanten
const knight_wait_timer : float = 1.8
const archer_wait_timer : float = 2.2
const loop_animation: bool = true

func _ready():
	skeleton1.play("AnimationDefeatScreenSkeleton/Skeleton1Idle")
	skeleton2.play("AnimationDefeatScreenSkeleton/Skeleton2Idle")
	wizard.play("AnimationDefeatScreenWizard/WizardIdle")
	knight.play("AnimationsDefeatScreenKnight/KnightIdle")
	archer.play("AnimationDefeatScreenArcher/ArcherIdle")
	skeleton1.queue("AnimationDefeatScreenSkeleton/Skeleton1Attack")
	skeleton2.queue("AnimationDefeatScreenSkeleton/Skeleton2Attack")
	timer_Knight.wait_time = knight_wait_timer
	timer_Knight.one_shot = true
	timer_Knight.autostart = true
	timer_archer.wait_time = archer_wait_timer
	timer_archer.one_shot = true
	timer_archer.autostart = true
	while not timer_Knight.timeout: 
		knight.queue("AnimationsDefeatScreenKnight/KnightIdle")
	while not timer_archer.timeout:
		archer.queue("AnimationDefeatScreenArcher/ArcherGoingDown") 
	if timer_Knight.timeout and knight.current_animation == "AnimationsDefeatScreenKnight/KnightIdle" :
		knight.stop(false)
		knight.play("AnimationsDefeatScreenKnight/KnightGoingDown")
		if skeleton1.current_animation == "AnimationDefeatScreenSkeleton/Skeleton1Attack" and skeleton1.animation_finished:
			while loop_animation == true:
				skeleton1.queue("AnimationDefeatScreenSkeleton/Skeleton1Idle")
				
	if timer_archer.timeout and archer.current_animation == "AnimationsDefeatScreenArcher/ArcherIdle" :
		archer.stop(false)
		archer.play("AnimationsDefeatScreenArcher/ArcherGoingDown")
		if skeleton2.current_animation == "AnimationDefeatScreenSkeleton/Skeleton2Attack" and skeleton2.animation_finished:
			while loop_animation == true:
				skeleton2.queue("AnimationDefeatScreenSkeleton/Skeleton2Idle")



# Callback-Funktion für den Tastendruck
func _on_button_pressed():
	get_tree().change_scene_to_file("res://scene/ui/MainMenu.tscn")
	
	

