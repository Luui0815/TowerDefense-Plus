using Godot;
using System;

public partial class Archer : RangeDefender
{
    AnimatedSprite2D _archer;

    public override void Init()
    {
        _name = "archer";
        _archer = GetNode<AnimatedSprite2D>("archer");
        _archer.Play();
    }
}