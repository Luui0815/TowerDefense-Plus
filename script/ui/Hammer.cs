using Godot;

public partial class Hammer : Control
{
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
