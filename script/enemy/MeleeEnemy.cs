using Godot;
using System;

public abstract partial class MeleeEnemy : Enemy
{
	protected string _currentTower;

	public string CurrentTower
	{
		get { return _currentTower; }
		set { _currentTower = value; }
	}

	public abstract void OnTowerEnteredBody();
	public abstract void Action();
	public abstract void Attack();
	
}
