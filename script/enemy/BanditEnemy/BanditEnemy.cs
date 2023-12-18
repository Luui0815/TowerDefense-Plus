using Godot;
using System;

public partial class BanditEnemy : MeleeEnemy
{
    AnimatedSprite2D _banditEnemy;
    Area2D _attackRangeArea;
    Timer _attackTimer;

    public BanditEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "BanditEnemy";
        WalkSpeed = 1f;
        Health = 8;
    }

    public override void _Ready()
    {
        _banditEnemy = GetNode<AnimatedSprite2D>("BanditEnemy");
        _banditEnemy.Play("walking");
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");

        _attackTimer = GetNode<Timer>("AttackTimer");
    }

    public override void _Process(double delta)
    {
        if (!CanAttack())
        {
            MoveEnemy(WalkSpeed);
        }

        if (Health <= 0)
        {
            Destroy();
        }


    }

    private bool CanAttack()
    {
        if (_attackTimer.IsStopped())
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
                WalkSpeed = 1f;
                _banditEnemy.Play("walking");
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
        _banditEnemy.Play("death");
        base.Destroy();
    }

    //public override void OnTowerEnteredBody(Node2D tower)
    //{}

    //public override void Action()
    //{}
}
