using Godot;
using System;

public partial class ArrowSpearProjectile : Projectile
{
    [Signal]
    public delegate void hitTargetEventHandler();

	private Vector2 _targetPosition;
	private Enemy _target;
	private Vector2 _velocity;
    private Area2D _HitboxArea;
	private AnimatedSprite2D _animatedSprite;
	private Node2D _projectile;
	private string _Mode;

	public void Init(Vector2 targetPosition, Enemy target, float velocity, string Mode, Vector2 Position)
	{
		_targetPosition = targetPosition;
		_target = target;
		_velocity.X = velocity;
		Visible = true;
		_Mode = Mode;
        TopLevel = true;
		this.Position = Position;
    }
	public override void _Ready()
	{
        _animatedSprite = GetNode<AnimatedSprite2D>("animatedSprite");
		_animatedSprite.Play(_Mode);
		if(_Mode=="arrow")
			_HitboxArea = GetNode<Area2D>("ArrowHitboxArea");
		else
            _HitboxArea = GetNode<Area2D>("SpearHitboxArea");

		_HitboxArea.Visible = true;

    }

	public override void _Process(double delta)
	{
		MoveProjectile(_velocity);//TODO: Pfeil auf Bahnen fliegen lassen

		if(CheckTarget())
		{
			EmitSignal(SignalName.hitTarget);
            QueueFree();
        }

		if (Position.X > _targetPosition.X && Position.Y < _targetPosition.Y + 35)//Falls Gegner schon besiegt wurde
		{
			float Dif= Position.X - _targetPosition.X;
			int t = Convert.ToInt32(Dif / 50);
			_velocity.Y = t+1;
        }

		if(Position.X>1018)
			QueueFree();
		//Objekt wird geloescht wenn in MapField Area geentred wird oder rechte Endposition erreicht wird
        float targetAngle = Mathf.Atan2(_velocity.Y, _velocity.X);
        Rotation = Mathf.LerpAngle(Rotation, targetAngle,4*(float)delta);//vielleicht die 2 Anpassen, damit sich Pfeil langsamer/schneller dreht

    }

	private bool CheckTarget()
	{
        foreach (Node2D body in _HitboxArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent as Enemy ==_target)
                {
					return true;
                }
            }
        }
		return false;
    }
}
