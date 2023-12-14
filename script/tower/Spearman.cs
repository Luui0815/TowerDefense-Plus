using Godot;
using System;

public partial class Spearman : MeleeDefender
{
	AnimatedSprite2D _spearman;
	public override void Init()
	{
        _name = "spearman";
        //_cost = get_Tower_cost(_name);
        _cost = 1000;

        _spearman = GetNode<AnimatedSprite2D>("spearman");
        _spearman.Play();
	}
}
