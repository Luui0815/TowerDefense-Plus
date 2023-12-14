using Godot;
using System;

public partial class FireTrap : TrapDefence
{
    AnimatedSprite2D _fire;

    public override void Init()
    {
        _name = "fire_trap";
        _fire = GetNode<AnimatedSprite2D>("fire_trap");
        _fire.Play();
    }
}