using Godot;
using System;
using System.Collections.Generic;

public partial class FireTrap : TrapDefence
{
	private List<Enemy> _attackableEnemiess = new List<Enemy>();
	public FireTrap()
	{
		_delay = 15;//nicht benoetigt, da Trap nur Status hinzufuegt
		_animationDelay = 1;
		_actionAnimation = "idle";
		Health = 6;
	}

	public override void Action()
	{

	}

	public override void _Ready()
	{
		_AttackArea = GetNode<Area2D>("AttackArea");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		_animatedSprite.Play(_actionAnimation);
	}

	public override void _Process(double delta)
	{
		_attackableEnemiess = SelectTargets();

		if(_attackableEnemiess.Count > 0)
		{
			foreach(Enemy enemy in _attackableEnemiess)
			{
				enemy.AddStatusEffect("burn");
            }
		}

		if(Health<=0)
		{
			OnDefenderDefeated();
		}
	}

	private List<Enemy> SelectTargets()
	{
		List<Enemy> EnemyList = new List<Enemy>();

		foreach (Node2D body in _AttackArea.GetOverlappingAreas())
		{
			Node2D parent = (Node2D)body.GetParent();
			if (parent is Enemy && body.Name == "HitboxArea")
			{
				EnemyList.Add(parent as Enemy);
			}
		}
		return EnemyList;
	}
	private void _on_attack_area_area_entered(Area2D area)
	{
		if(area.GetParent() is Enemy)
            Health--;
    }
}






