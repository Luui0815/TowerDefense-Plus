using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

namespace TowerDefense
{
    public struct Statuseffects
    {
        public string name;
        public Timer DamageTimer;
		public Timer DelayTimer;
        public int damage;

        public bool Contains(string name)
        {
            if (this.name == name)
                return true;
            else return false;
        }

        public Statuseffects(string name, int Timelength, int damage)
        {
            this.name = name;

            DamageTimer = new Timer();
			DamageTimer.OneShot = true;
			DamageTimer.WaitTime = Timelength;

			DelayTimer=new Timer(); ;
			DelayTimer.OneShot = true;
			DelayTimer.WaitTime = 1.5f;

			this.damage = damage;
        }
    }
}

public abstract partial class Enemy : GameEntity
{
    protected bool _enemyDefeated = false;
	protected bool _enemyCrossedLastField = false;
	protected float _walkSpeed;
	protected string _name;
	protected bool _TrapDeleted = false;
	protected List<Statuseffects> _statusEffects = new List<Statuseffects>();
	protected AnimatedSprite2D _BurnAnimation;

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

    public override void Action()
    {

    }

	public void DeleteTrap(string trap)
	{
        _TrapDeleted = true;
		string effect;
		switch (trap)
		{
			case "FireTrapDefender":
				{
					effect = "burn";
					break;
				}
			case "caltrop_trap":
				{
					effect = "caltrop";
					break;
				}
			default:
				{
					effect = "";
					break;
				}
		}
        _statusEffects.First(x => x.name == effect).DamageTimer.Stop();
        _statusEffects.First(x => x.name == effect).DelayTimer.Stop();

    }
	
	public virtual void AddStatusEffect(string effect)
	{
		bool contained=false;

		foreach(Statuseffects statuseffect in _statusEffects)
		{
			if(statuseffect.Contains(effect))
			{
				contained= true;
			}
		}

		switch (effect)
		{
			case "burn":
				{ 
                    if (!contained)
					{
                        Statuseffects stat = new Statuseffects("burn", 10, 1);
						AddChild(stat.DamageTimer);
						AddChild(stat.DelayTimer);

						stat.DamageTimer.Start();
						stat.DelayTimer.Start();

                        _statusEffects.Add(stat);
					}
					else
					{
                        _statusEffects.First(x => x.name == effect).DamageTimer.Start();
                    }
					break;
				}
			case "caltrop":
				{
					if (!contained)
					{
                        Statuseffects stat = new Statuseffects("caltrop", 5, 2);
                        AddChild(stat.DamageTimer);
                        AddChild(stat.DelayTimer);

                        stat.DamageTimer.Start();
                        stat.DelayTimer.Start();

                        _statusEffects.Add(stat);
                    }
					else
					{
						if(_statusEffects.First(x => x.name == effect).DamageTimer.IsStopped())
						{
                            _statusEffects.First(x => x.name == effect).DamageTimer.Start();
                        }
					}
                    
                    break;
                }

        }
	}

    public override void Destroy()
    {
		QueueFree();
    }

	public virtual void getStatuseffectDamage() //wenn true dann burn damage fuer burn animation der Gegner
	{
		foreach(Statuseffects effect in _statusEffects)
		{
			if(!effect.DamageTimer.IsStopped() && effect.DelayTimer.IsStopped())
			{
				//check if trap still exists
				if (!_TrapDeleted)
				{
					Health -= effect.damage;
					effect.DelayTimer.Start();
				}
			}
		}
	}
	public bool IsFreezed()
	{
        bool caltrop = false;
        //check if caltrap effect is theire
        foreach (Statuseffects effect in _statusEffects)
        {
            if (effect.name == "caltrop" && !effect.DamageTimer.IsStopped())
            {
                caltrop = true;
            }
        }
        return caltrop;
    }

    public bool IsBurned()
    {
        bool burn = false;
        //check if burn effect is theire
        foreach (Statuseffects effect in _statusEffects)
        {
            if (effect.name == "burn" && !effect.DamageTimer.IsStopped())
            {
                burn = true;
            }
        }
        return burn;
    }

    protected void MoveEnemy(float WalkSpeed)
    {
		Vector2 movement = new Vector2(0, 0);

		if(!IsFreezed())
		{
			movement = new(-WalkSpeed, 0);
		}

        Translate(movement);
    }
}
