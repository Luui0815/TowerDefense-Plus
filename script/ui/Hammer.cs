using Godot;
using System;

public partial class Hammer : Control
{
    private bool _dragPreview;
    private AnimatedSprite2D _animatedSprite;

    public void setAnimation(bool DragPreview)
    {
        if (DragPreview)
            _dragPreview = true;
        else
            _dragPreview = false;
    }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("animatedSprite");
        if (_dragPreview)
            _animatedSprite.Play("action");
        else
            _animatedSprite.Play("idle");
    }

    public override void _Process(double delta)
    {
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        SetDragPreview(CreateDragPreview());
        return "hammer";
    }

    private Control CreateDragPreview()
    {
        Hammer h;
        h = (Hammer)GD.Load<PackedScene>("res://scene/ui/Hammer.tscn").Instantiate();
        h.setAnimation(true);
        h.TopLevel= true;
        return h;
    }
}
