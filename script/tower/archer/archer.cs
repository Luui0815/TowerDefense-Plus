using Godot;
using System;

public partial class archer : rangeDefender
{
    AnimatedSprite2D _archer;

    public override void Init()
    {
        _name = "archer";
        //_cost = get_Tower_cost(_name);
        _cost = 500;

        _archer = GetNode<AnimatedSprite2D>("archer");
        _archer.Play();
    }

    public override void _Process(double delta)
    {
    }
}