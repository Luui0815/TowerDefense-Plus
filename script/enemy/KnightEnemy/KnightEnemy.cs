using Godot;
using System;
using System.Diagnostics;

public partial class KnightEnemy : MeleeEnemy
{
	AnimatedSprite2D _knightEnemy;
	Area2D _attackRangeArea, _hitboxArea;
	Timer _attackTimer;

	public KnightEnemy()
	{
		_delay = 15;
		_animationDelay = 1;
		_actionAnimation = "idle";

		EnemyName = "KnightEnemy";
		WalkSpeed = 0.3f;
		Health = 25;
	}

	public override void _Ready()
	{
		_knightEnemy = GetNode<AnimatedSprite2D>("KnightEnemy");
		_knightEnemy.Play("walking");
        _knightEnemy.AnimationLooped += OnAnimationLooped;
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");
        _hitboxArea = GetNode<Area2D>("HitboxArea");

        _attackTimer = GetNode<Timer>("AttackTimer");
        _BurnAnimation = GetNode<AnimatedSprite2D>("burn");
    }

	public override void _Process(double delta)
	{
		getStatuseffectDamage();

        if (IsBurned())//ist true wenn burn damage
        {
            _BurnAnimation.Play("burn");
            _BurnAnimation.Visible = true;
        }
        else
        {
            _BurnAnimation.Visible = false;
            _BurnAnimation.Stop();
        }

        if (!CanAttack() && !EnemyDefeated)
		{
			MoveEnemy(WalkSpeed);
		}

		if(Health <=0 && !EnemyDefeated) 
		{
			OnEnemyDefeated();
		}
	}

	private bool CanAttack()
	{
		Vector2 _BurnAnimationPositionWalking = new Vector2(-5,-35);
		Vector2 _BurnAnimationPositionAttacking = new Vector2(15, -35);

		if(IsFreezed())
		{
            if (_knightEnemy.Animation == "attacking" || _knightEnemy.Animation == "walking")
                _knightEnemy.Play("idle");
			return false;
		}

		if (_attackTimer.IsStopped() && !EnemyDefeated)
		{
			Defender closestTarget = SelectClosestTarget(_attackRangeArea);
			if (closestTarget != null && !closestTarget.ImmuneToDamage)
			{
				WalkSpeed = 0;
				_attackTimer.Start();
				_BurnAnimation.Position = _BurnAnimationPositionAttacking;
				_knightEnemy.Play("attacking");
				Attack(closestTarget, 1);
				return true;
			}
			else
			{
				if(!EnemyDefeated)
				{
					WalkSpeed = 0.3f;
                    if (!IsFreezed())
					{
						_BurnAnimation.Position= _BurnAnimationPositionWalking;
                        _knightEnemy.Play("walking");
					}
                    else
                        _knightEnemy.Play("idle");
                }
                MoveEnemy(WalkSpeed);
                return false;
			}
		}
		else
		{
			return false;
		}
	}

    private void OnEnemyDefeated()
    {
        WalkSpeed = 0;
        EnemyDefeated = true;
        _hitboxArea.QueueFree();
        _knightEnemy.Play("death");
    }
    private void OnAnimationLooped()
    {
        if (_knightEnemy.Animation == "death")
        {
            GameLevel Level = (GameLevel)GetParent().GetParent();
            Level.AddMoney(25);
            Destroy();
        }
    }
}
