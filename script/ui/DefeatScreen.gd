extends Node

# erstelle Zugriffsreferenzen auf alle Animationsplayer der 5 Figuren
@onready var knight = $"Panel/FallenKnight/AnimationPlayer"
@onready var archer = $"Panel/FallenArcher/AnimationPlayer"
@onready var wizard = $"Panel/VictoriousWizard/AnimationPlayer"
@onready var skeleton1 = $"Panel/VictoriousSkeleton1/AnimationPlayer"
@onready var skeleton2 = $"Panel/VictoriousSkeleton2/AnimationPlayer"

#Startwerte für Attack-Zeiten
var time_skeleton1: float = 0
var time_skeleton2: float = 0

func _on_ready_animation(): #setze alle Startanimationen, wenn der Niederlagenscreen geöffnet wird
	knight.play("AnimationsDefeatScreenKnight/KnightIdle")
	archer.play("AnimationDefeatScreenArcher/ArcherIdle")
	wizard.play("AnimationDefeatScreenWizard/WizardIdle")
	skeleton1.play("AnimationDefeatScreenSkeleton/Skeleton1Idle")
	skeleton2.play("AnimationDefeatScreenSkeleton/Skeleton2Idle")

func _start_defeat_animation(): #startet Angriffsanimation der Skelete und triggert Todesanimation von Archer und Knight
	#animation_set_next() definiert die nächste Animation, die in der Queue folgend angesteuert wird und ausgeführt
	#Syntax animation_set_next(aktuelle Animation als String, Zielanimation als Sring)
	skeleton1.animation_set_next("AnimationDefeatScreenSkeleton/Skeleton1Idle", "AnimationDefeatScreenSkeleton/Skeleton1Attack")
	skeleton2.animation_set_next("AnimationDefeatScreenSkeleton/Skeleton2Idle", "AnimationDefeatScreenSkeleton/Skeleton2Attack")
	# Wenn du Animation nicht aus Idle rausgeht von Knight und Archer sowie die Skelete nur eine Atackk starten und dann freezen muss hier eine schleife rein
	# Diese Schleife muss solange Laufen, bis ein mal die Attack Animation durchgelaufen ist
	# also sowas z.B. wie: while not skeleton1.animation_finished() (Arbeitsblock) do
	# kann kann die if Abfrage mit der derzeitigen Animationszeit erfolgreich abgefangen werden
	
	#Vergleich ob aktuelle Animation die Attack-Animation ist für Zeitvergleich
	if skeleton1.current_animation("AnimationDefeatScreenSkeleton/Skeleton1Attack"):
		# Zuweisung der aktuellen Animationssequenzzeit in float Variable über curren_animation_position
		time_skeleton1 = skeleton1.current_animation_position
		if time_skeleton1 == 0.7:
			# Stop und Zurücksetzen der aktuellen Animation mit stop(flase)
			knight.stop(false)
			knight.play("AnimationsDefeatScreenKnight/KnightGoingDown")
			#Bafrage ob Attack Animation zu ende mit animation_finished()
			if skeleton1.animation_finished():
				skeleton1.play("AnimationDefeatScreenSkeleton/Skeleton1Idle")
	# Wenn du Animation nicht aus Idle rausgeht von Knight und Archer sowie die Skelete nur eine Atackk starten und dann freezen muss hier eine schleife rein
	# Diese Schleife muss solange Laufen, bis ein mal die Attack Animation durchgelaufen ist
	# also sowas z.B. wie: while not skeleton1.animation_finished() (Arbeitsblock) do
	# kann kann die if Abfrage mit der derzeitigen Animationszeit erfolgreich abgefangen werden
	if skeleton2.current_animation("AnimationDefeatScreenSkeleton/Skeleton2Attack"):
		time_skeleton2 = skeleton2.current_animation_length
		if time_skeleton2 == 0.9:
			archer.stop(false)
			archer.play("AnimationDefeatScreenArcher/ArcherGoingDown")
			if skeleton2.animation_finished():
				skeleton2.play("AnimationDefeatScreenSkeleton/Skeleton2Idle")

func _ready(): # Ist wie Main-Teil von C# Programm. Ab hier läuft das gesamte Script
	_on_ready_animation()
	_start_defeat_animation()

func _on_button_pressed():
	get_tree().change_scene_to_file("res://scene/ui/MainMenu.tscn")
