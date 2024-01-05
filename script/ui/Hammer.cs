using Godot;

public partial class Hammer : Control
{
    [Export]
    private bool Animated = false;
    private AnimatedSprite2D _animatedSprite;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("animatedSprite");
        string animation = Animated ? "action" : "idle";
        _animatedSprite.Play(animation);
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        SetDragPreview(CreateDragPreview());
        return "hammer";
    }

    private Control CreateDragPreview()
    {
        return (Control)GD.Load<PackedScene>("res://scene/ui/HammerPreview.tscn").Instantiate();
    }
}
