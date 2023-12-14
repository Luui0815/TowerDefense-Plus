using Godot;
using System;

public partial class Wall : Defender
{
    AnimatedSprite2D _wall;

    public override void Init()
    {
        _name = "wall";
        _wall = GetNode<AnimatedSprite2D>("wall");
        _wall.Play();
    }
}
