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
		private float _maxProjectileY;

		public void Init(Enemy target, float velocity, ProjectileType type)
		{
			Init(target, velocity);
			_maxProjectileY = _targetPosition.Y + 35;
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
			Translate(_velocity);//TODO: Pfeil auf Bahnen fliegen lassen

			if(HasTarget())
			{
				EmitSignal(Projectile.SignalName.TargetHit);
				QueueFree();
			}

			//Falls Gegner besiegt wurde, Pfeil nach unten ablenken
			if (Position.X > _targetPosition.X)
			{
				float positionDif = Position.X - _targetPosition.X;
				int dropDownFactor = Convert.ToInt32(positionDif / 50);
				_velocity.Y = dropDownFactor+1;
			}

			//Objekt wird geloescht wenn in MapField Area geentred wird oder rechte Endposition erreicht wird
			if(Position.X>1018)
			{
				QueueFree();
			}
			
			float targetAngle = Mathf.Atan2(_velocity.Y, _velocity.X);
			Rotation = Mathf.LerpAngle(Rotation, targetAngle,4*(float)delta);
		}
	}
}
