using Godot;
using System;

public partial class Spearman : MeleeDefender
{
	AnimatedSprite2D _spearman;
	public override void Init()
	{
        _name = "spearman";
        _spearman = GetNode<AnimatedSprite2D>("spearman");
        _spearman.Play();
	}
}
