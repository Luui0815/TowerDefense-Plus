using Godot;

public partial class LawnMower : CharacterBody2D
{
	private Area2D _hitboxArea;
	private AnimatedSprite2D _animatedSprite;
	private bool _activated;

	public override void _Ready()
	{
		_hitboxArea = GetNode<Area2D>("HitboxArea");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		_animatedSprite.Play("idle");
	}

	public override void _Process(double delta)
	{
		if(_activated)
		{
			Move();
			DestroyEnemyUnits();
		}
	}

    private void Move()
	{
		_animatedSprite.Play("action");
		Vector2 movement = new(5, 0);
		Translate(movement);
		if (Position.X > 1200)
			QueueFree();
	}

	private void DestroyEnemyUnits()
	{
        foreach (Node2D body in _hitboxArea.GetOverlappingAreas())
        {
            if (body.GetParent() is Enemy && body.Name == "HitboxArea")
            {
                Enemy target = (Enemy)body.GetParent();
				target.Health -= 1000;
				//Ondefeated Methode ware auch moeglich, ist aber private und so nicht erreichbar
            }
        }
    }

	public bool LawnMoverActivated
	{
		get
		{
			return _activated;
		}
		set
		{
			_activated = true;
		}
	}
}
