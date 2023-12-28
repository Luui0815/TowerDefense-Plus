using Godot;
using System;

public partial class BanditEnemy : MeleeEnemy
{
    AnimatedSprite2D _banditEnemy;
    Area2D _attackRangeArea, _hitboxArea;
    Timer _attackTimer;

    public BanditEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "BanditEnemy";
        WalkSpeed = 1.1f;
        Health = 12;
    }

    public override void _Ready()
    {
        _banditEnemy = GetNode<AnimatedSprite2D>("BanditEnemy");
        _banditEnemy.Play("walking");
        _banditEnemy.AnimationLooped += OnAnimationLooped;
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
        if (_attackTimer.IsStopped() && !EnemyDefeated)
        {
            Defender closestTarget = SelectClosestTarget(_attackRangeArea);
            if (closestTarget != null && !closestTarget.ImmuneToDamage)
            {
                WalkSpeed = 0;
                _attackTimer.Start();
                _banditEnemy.Play("attacking");
                Attack(closestTarget, 1);
                return true;
            }
            else
            {
                if (!EnemyDefeated)
                {
                    WalkSpeed = 1.1f;
                    if (!IsFreezed())
                        _banditEnemy.Play("walking");
                    else
                        _banditEnemy.Play("idle");
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
        _banditEnemy.Play("death");
    }

    private void OnAnimationLooped()
    {
        if(_banditEnemy.Animation =="death")
        {
            Destroy();
        }
    }
}
