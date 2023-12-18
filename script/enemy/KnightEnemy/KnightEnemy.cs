using Godot;
using System;
using System.Diagnostics;

public partial class KnightEnemy : MeleeEnemy
{
	AnimatedSprite2D _knightEnemy;
	Area2D _attackRangeArea;
	Timer _attackTimer;

	public KnightEnemy()
	{
		//TODO: Change values
		_delay = 15;
		_animationDelay = 1;
		_actionAnimation = "idle";

		EnemyName = "KnightEnemy";
		WalkSpeed = 0.4f;
		Health = 10;
	}

	public override void _Ready()
	{
		_knightEnemy = GetNode<AnimatedSprite2D>("KnightEnemy");
		_knightEnemy.Play("walking");
		_attackRangeArea = GetNode<Area2D>("AttackRangeArea");

		_attackTimer = GetNode<Timer>("AttackTimer");
	}

	public override void _Process(double delta)
	{
		getStatuseffectDamage();

		if (!CanAttack())
		{
			MoveEnemy(WalkSpeed);
		}

		if(Health <=0 && !EnemyDefeated) 
		{
			Destroy();
		}
	}

	private bool CanAttack()
	{
		if (_attackTimer.IsStopped())
		{
			Defender closestTarget = SelectClosestTarget(_attackRangeArea);
			if (closestTarget != null)
			{
				WalkSpeed = 0;
				_attackTimer.Start();
				_knightEnemy.Play("attacking");
				Attack(closestTarget, 1);
				return true;
			}
			else
			{
				WalkSpeed = 0.4f;
				_knightEnemy.Play("walking");
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	public override void Destroy()
	{
		EnemyDefeated = true;
		_knightEnemy.Play("death");
		base.Destroy();
	}

	//public override void OnTowerEnteredBody(Node2D tower)
	//{}

	//public override void Action()
	//{}
	
}
