extends Node

#Script ist inhaltlich zu defeat_scree von angesprochenen Objekten und Animationseigenschaften ähnlich
#Nur sollen hier Knight + Archer angreifen und Wizard + Skelette sterben in Animation

# erstelle Zugriffsreferenzen auf alle Animationsplayer der 5 Figuren
# da Namen 
@onready var knight = $"Panel/FallenKnight/AnimationPlayer"
@onready var archer = $"Panel/FallenArcher/AnimationPlayer"
@onready var wizard = $"Panel/VictoriousWizard/AnimationPlayer"
@onready var skeleton1 = $"Panel/VictoriousSkeleton1/AnimationPlayer"
@onready var skeleton2 = $"Panel/VictoriousSkeleton2/AnimationPlayer"

#Startwerte für Attack-Zeiten
var time_knight: float = 0
var time_archer: float = 0

func _on_ready_animation(): #setze alle Startanimationen, wenn der Niederlagenscreen geöffnet wird
	knight.play("AnimationsDefeatScreenKnight/KnightIdle")
	archer.play("AnimationDefeatScreenArcher/ArcherIdle")
	wizard.play("AnimationDefeatScreenWizard/WizardIdle")
	skeleton1.play("AnimationDefeatScreenSkeleton/Skeleton1Idle")
	skeleton2.play("AnimationDefeatScreenSkeleton/Skeleton2Idle")

#detalierte Kommentierung siehe Script defeat_screen
func _start_defeat_animation(): #startet Angriffsanimation der Skelete und triggert Todesanimation von Archer und Knight
	knight.animation_set_next("AnimationDefeatScreenKnight/KnightIdle", "AnimationDefeatScreenKnight/KnightAttack")
	archer.animation_set_next("AnimationDefeatScreenArcher/ArcherIdle", "AnimationDefeatScreenArcher/ArcherAttack")
	# Wenn du Animation nicht aus Idle rausgeht von Knight und Archer sowie die Skelete nur eine Atackk starten und dann freezen muss hier eine schleife rein
	# Diese Schleife muss solange Laufen, bis ein mal die Attack Animation durchgelaufen ist
	# also sowas z.B. wie: while not skeleton1.animation_finished() (Arbeitsblock) do
	# kann kann die if Abfrage mit der derzeitigen Animationszeit erfolgreich abgefangen werden
	if knight.current_animation("AnimationDefeatScreenKnight/KnightAttack"):
		time_knight = knight.current_animation_length
		if time_knight == 0.7:
			skeleton1.stop(false)
			skeleton2.stop(false)
			skeleton1.play("AnimationDefeatScreenSkeleton/SkeletonGoingDown")
			skeleton2.play("AnimationDefeatScreenSkeleton/SkeletonGoingDown")
			if knight.animation_finished():
					knight.play("AnimationsDefeatScreenKnight/KnightIdle")
	# Wenn du Animation nicht aus Idle rausgeht von Knight und Archer sowie die Skelete nur eine Atackk starten und dann freezen muss hier eine schleife rein
	# Diese Schleife muss solange Laufen, bis ein mal die Attack Animation durchgelaufen ist
	# also sowas z.B. wie: while not skeleton1.animation_finished() (Arbeitsblock) do
	# kann kann die if Abfrage mit der derzeitigen Animationszeit erfolgreich abgefangen werden
	if archer.current_animation("AnimationDefeatScreenArcher/ArcherAttack"):
		time_archer = archer.current_animation_position
		if time_archer == 0.5:
			wizard.stop(false)
			wizard.play("AnimationDefeatScreenWizard/WizardGoingDown")
			if archer.animation_finished():
				archer.play("AnimationDefeatScreenArcher/ArcherIdle")

# Called when the node enters the scene tree for the first time.
func _ready():
	_on_ready_animation()
	_start_defeat_animation()

func _on_button_pressed():
	get_tree().change_scene_to_file("res://scene/ui/MainMenu.tscn")
