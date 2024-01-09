using Godot;
using System;

public partial class GruntEnemy : MeleeEnemy
{
    AnimatedSprite2D _gruntEnemy;
    Area2D _attackRangeArea, _hitboxArea;
    Timer _attackTimer;

    public GruntEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "GruntEnemy";
        WalkSpeed = 1.2f;
        Health = 8;
    }

    public override void _Ready()
    {
        _gruntEnemy = GetNode<AnimatedSprite2D>("GruntEnemy");
        _gruntEnemy.Play("walking");
        _gruntEnemy.AnimationLooped += OnAnimationLooped;
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

        if (Health <= 0 && !EnemyDefeated)
        {
            OnEnemyDefeated();
        }


    }

    private bool CanAttack()
    {
        if (IsFreezed())
        {
            if (_gruntEnemy.Animation == "attacking" || _gruntEnemy.Animation == "walking")
                _gruntEnemy.Play("idle");
            return false;
        }

        if (_attackTimer.IsStopped() && !EnemyDefeated)
        {
            Defender closestTarget = SelectClosestTarget(_attackRangeArea);
            if (closestTarget != null && !closestTarget.ImmuneToDamage)
            {
                WalkSpeed = 0;
                _attackTimer.Start();
                _gruntEnemy.Play("attacking");
                Attack(closestTarget, 2);
                return true;
            }
            else
            {
                if (!EnemyDefeated)
                {
                    WalkSpeed = 1.2f;
                    if (!IsFreezed())
                        _gruntEnemy.Play("walking");
                    else
                        _gruntEnemy.Play("idle");
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
        _gruntEnemy.Play("death");
    }

    private void OnAnimationLooped()
    {
        if (_gruntEnemy.Animation == "death")
        {
            GameLevel Level = (GameLevel)GetParent().GetParent();
            Level.AddMoney(25);
            Destroy();
        }
    }
}
