using Godot;
using System;

public abstract partial class MeleeEnemy : Enemy
{
	protected Node _currentTower;

	public Node CurrentTower
	{
		get { return _currentTower; }
		set { _currentTower = value; }
	}

    public virtual void OnTowerEnteredBody(Node2D tower)
    {

	}
	public override void Action()
	{

	}
	public virtual void Attack(Defender closestTarget)
	{
		closestTarget.Health -= 1;
	}
	
}
