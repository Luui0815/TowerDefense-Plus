using Godot;
using System;

public abstract partial class AttackTower : Defender
{
	protected int _damage;
    protected Timer _AttackTimer;

    protected void Attack(Enemy target, int damage)
    {
        target.Health -= damage;
    }
}