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
        WalkSpeed = 0.5f;
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
        if (!CanAttack())
        {
            MoveEnemy();
        }

        if(Health <=0) 
        {
            Destroy();
        }

       
	}

    private bool CanAttack()
    {
        if (_attackTimer.IsStopped())
        {
            Defender closestTarget = SelectClosestTarget();
            if (closestTarget != null)
            {
                WalkSpeed = 0;
                _attackTimer.Start();
                _knightEnemy.Play("attacking");
                Attack(closestTarget);
                return true;
            }
            else
            {
                WalkSpeed = 0.5f;
                _knightEnemy.Play("walking");
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
        
        foreach(Node2D body in _attackRangeArea.GetOverlappingAreas())     
        {
            Node2D parent = (Node2D)body.GetParent();
            if(parent.HasMethod("GetTowerCost"))
            {
                float distance = Position.DistanceTo(parent.Position);
                if(distance < closestDistance) 
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
        _knightEnemy.Play("death");
        base.Destroy();     
    }

    //public override void OnTowerEnteredBody(Node2D tower)
    //{}

    //public override void Action()
    //{}
    
}
