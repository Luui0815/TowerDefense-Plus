using Godot;
using System;

public partial class Wall : Defender
{
    AnimatedSprite2D _wall;

    public override void Init()
    {
        _name = "wall";
        //_cost = get_Tower_cost(_name);
        _cost = 250;

        _wall = GetNode<AnimatedSprite2D>("wall");
        _wall.Play();
    }

    public override void _Process(double delta)
    {
    }
}
