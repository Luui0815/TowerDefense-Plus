using Godot;
using System;

public partial class spearman : defender
{
	AnimatedSprite2D _spearman;
	public override void Init()
	{
        _cost = 999;
        _name = "spearman";

        _spearman = GetNode<AnimatedSprite2D>("spearman");
        _spearman.Play();
	}
}
