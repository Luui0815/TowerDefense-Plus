using Godot;
using System;

public abstract partial class Enemy : Node2D
{
	protected bool _enemyDefeated = false;
	protected bool _enemyCrossedLastField = false;
	protected float _walkSpeed;
	protected string _name;
	//_statusEffects;

	public bool EnemyDefeated
	{
		get { return _enemyDefeated; }
		set { _enemyDefeated = value;}
	}

	public bool EnemyCrossedLastLane
	{
		get { return _enemyCrossedLastField; }
		set { _enemyCrossedLastField = value;}
	}

	public float WalkSpeed
	{
		get { return _walkSpeed; }
		set { _walkSpeed = value; }
	}

	public string EnemyName
	{
		get { return _name; }
		set { _name = value; }
	}

	public abstract void Init();
	//AddStatusEffect()
}
