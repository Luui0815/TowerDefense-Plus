using Godot;
using System;

public partial class DropTarget : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        string Info = data.ToString();
        Dropitem di2 = (Dropitem)GD.Load<PackedScene>("res://scene/map/Dropitem.tscn").Instantiate();

        //Dropitem di = (Dropitem)data;
        AddChild(di2);
    }
}
