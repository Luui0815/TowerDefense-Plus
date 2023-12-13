using Godot;
using System;

public partial class MeleeEnemy : Enemy
{
	protected Node _currentTower;

	public Node CurrentTower
	{
		get { return _currentTower; }
		set { _currentTower = value; }
	}

	public virtual void OnTowerEnteredBody()
	{

	}
	public override void Action()
	{

	}
	public virtual void Attack()
	{

	}
	
}
