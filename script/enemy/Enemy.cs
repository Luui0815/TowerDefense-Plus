using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Enemy : GameEntity
{
	protected bool _enemyDefeated = false;
	protected bool _enemyCrossedLastField = false;
	protected float _walkSpeed;
	protected string _name;
	protected List<string> _statusEffects = new List<string>();

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

	public virtual void Init()
	{

	}
	
	public virtual void AddStatusEffect(string effect)
	{
		_statusEffects.Add(effect);
	}

    public override void Destroy()
    {
        
    }
}
