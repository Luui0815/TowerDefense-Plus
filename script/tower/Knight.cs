using Godot;
using System;

public  partial class Knight : MeleeDefender
{
	AnimatedSprite2D _knight;

	public override void Init()
	{
        _name = "knight";
        _knight = GetNode<AnimatedSprite2D>("Knight");
        _knight.Play();
    }
}
