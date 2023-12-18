using Godot;
using System;

public partial class GiantEnemy : MeleeEnemy
{
    AnimatedSprite2D _giantEnemy;
    Area2D _attackRangeArea;
    Timer _attackTimer;

    public GiantEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "GiantEnemy";
        WalkSpeed = 0.3f;
        Health = 50;
    }

    public override void _Ready()
    {
        _giantEnemy = GetNode<AnimatedSprite2D>("GiantEnemy");
        _giantEnemy.Play("walking");
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");

        _attackTimer = GetNode<Timer>("AttackTimer");
    }

    public override void _Process(double delta)
    {
        if (!CanAttack())
        {
            MoveEnemy();
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
            Defender closestTarget = SelectClosestTarget();
            if (closestTarget != null && !closestTarget.ImmuneToDamage)
            {
                WalkSpeed = 0;
                _attackTimer.Start();
                _giantEnemy.Play("attacking");
                Attack(closestTarget, 5);
                return true;
            }
            else
            {
                WalkSpeed = 0.3f;
                _giantEnemy.Play("walking");
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private Defender SelectClosestTarget()
    {
        Defender closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Node2D body in _attackRangeArea.GetOverlappingAreas())
        {
            Node2D parent = (Node2D)body.GetParent();
            if (parent.HasMethod("GetTowerCost"))
            {
                float distance = Position.DistanceTo(parent.Position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = parent as Defender;
                }
            }
        }
        return (Defender)closestTarget;
    }

    private void MoveEnemy()
    {
        Vector2 movement = new(-WalkSpeed, 0);
        Translate(movement);
    }

    public override void Destroy()
    {
        _giantEnemy.Play("death");
        base.Destroy();
    }

    //public override void OnTowerEnteredBody(Node2D tower)
    //{}

    //public override void Action()
    //{}
}
