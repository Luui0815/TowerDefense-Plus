using Godot;
using System;
using System.Transactions;

public partial class LawnMower : CharacterBody2D
{
	Area2D _attackRangeArea;
    private bool _activated = false;

    public LawnMower() 
    { 
        
    }
	public override void _Ready()
	{
		_attackRangeArea = GetNode<Area2D>("AttackRangeArea");
	}

	public override void _Process(double delta)
	{
        if(SearchTarget() && ! _activated)
        {
            _activated = true;
            StartMoving();
        }
	}

    protected bool SearchTarget()
    {
        foreach (Node2D body in _attackRangeArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent is Enemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void StartMoving() 
    {
        while(GlobalPosition.X < 1250)
        {
            MoveUnit();
            if (CanAttack())
            {
                Attack(_targetEnemy, _damage);
                _AttackTimer.Start();
            }
        }
    }

    private void MoveUnit()
	{
		Vector2 movement = new(5, 0);
		Translate(movement);
	}
}
