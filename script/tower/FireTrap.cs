using Godot;
using System;

public partial class FireTrap : TrapDefence
{
    AnimatedSprite2D _fire;

    public override void Init()
    {
        _name = "fire_trap";
        //_cost = get_Tower_cost(_name);
        _cost = 50;

        _fire = GetNode<AnimatedSprite2D>("fire_trap");
        _fire.Play();
    }

    public override void _Process(double delta)
    {
    }
}