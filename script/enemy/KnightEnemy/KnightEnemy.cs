using Godot;
using System;

public partial class KnightEnemy : MeleeEnemy
{
    AnimatedSprite2D _knightEnemy;
	public override void Init()
	{
        EnemyName = "KnightEnemy";
        WalkSpeed = 1;
        _knightEnemy = GetNode<AnimatedSprite2D>("KnightEnemy");
        _knightEnemy.Play("walking");
	}

	public override void _Process(double delta)
	{
	}

    public override void OnTowerEnteredBody()
    {
     
    }

    public override void Action()
    {

    }

    public override void Attack()
    {

    }
    public override void Destroy()
    {
        _knightEnemy.Play("death");
        base.Destroy();     
    }
}
