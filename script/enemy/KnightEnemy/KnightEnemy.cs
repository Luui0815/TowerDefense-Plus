using Godot;
using System;

public partial class KnightEnemy : MeleeEnemy
{
    AnimatedSprite2D _knightEnemy;
    Area2D _attackRangeArea;
    Timer _attackTimer;

	public override void Init()
	{
        EnemyName = "KnightEnemy";
        WalkSpeed = 0.5f;
        Health = 10;
        _knightEnemy = GetNode<AnimatedSprite2D>("KnightEnemy");
        _knightEnemy.Play("walking");
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");

        _attackTimer = GetNode<Timer>("AttackTimer");
	}

    public override void _Ready()
    {
        Init();
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
                _attackTimer.Start();
                Attack(closestTarget);
                return true;
            }
            else
            {
                _knightEnemy.Play("walking");
                return false;
            }
        }
        else return false;
    }
    
    private Defender SelectClosestTarget()
    {
        float closestDistance = float.MaxValue;
        Node2D closestTarget = null;
        
        foreach(Node2D body in _attackRangeArea.GetOverlappingAreas())     
        { 
            if(body is Defender defenderUnit)
            {
                float distance = Position.DistanceTo(defenderUnit.Position);
                if(distance < closestDistance) 
                {
                    closestDistance = distance;
                    closestTarget = body;
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
