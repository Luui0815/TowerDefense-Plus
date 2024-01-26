using Godot;
using System;

namespace TowerDefense
{
	public enum ProjectileType
	{
		Arrow,
		Spear
	}

	public partial class TowerProjectile : Projectile
	{
		private ProjectileType _type;
		private Defender _shooter;
		private Vector2 _dropPosition;
		private bool _shouldFall = false;

		public bool ShouldFall
		{
			get
			{
				return _shouldFall;
			}

			set
			{
				_shouldFall = value;
				_dropPosition = Position;
			}
		}

		public Defender Shooter
		{
			get 
			{
				return _shooter;
			}
		}

		public void Init(Enemy target, float velocity, ProjectileType type, Defender shooter)
		{
			Init(target, velocity);
			_shooter = shooter;
			_type = type;
		}

		public override void _Ready()
		{
			_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
			_animatedSprite.Play(_type.ToString());
			_hitboxArea = GetNode<Area2D>(_type + "HitboxArea");
		}

		public override void _Process(double delta)
		{
			Translate(_velocity);

			if(HasTarget())
			{
				EmitSignal(Projectile.SignalName.TargetHit);
				QueueFree();
			}

			//Falls Gegner besiegt wurde, Pfeil nach unten ablenken
			if (Position.X > _targetPosition.X)
			{
				ShouldFall = true;
			}

			//Objekt wird geloescht wenn in MapField Area geentred wird oder rechte Endposition erreicht wird
			if(Position.X>1018)
			{
				QueueFree();
			}

			if (ShouldFall){
				DropProjectile();
			}
			
			float targetAngle = Mathf.Atan2(_velocity.Y, _velocity.X);
			Rotation = Mathf.LerpAngle(Rotation, targetAngle,5*(float)delta);
		}

		private void DropProjectile()
		{
			float positionDif = Position.X - _dropPosition.X;
			int dropDownFactor = Convert.ToInt32(positionDif / 50);
			_velocity.Y = dropDownFactor + 2;
		}
	}
}
