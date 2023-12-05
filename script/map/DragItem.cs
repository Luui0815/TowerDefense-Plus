using Godot;
using System;

public partial class DragItem : Control
{
	[Export]
	private DragItem di;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		string Info;
		Dropitem di2 = (Dropitem)GD.Load<PackedScene>("res://scene/map/Dropitem.tscn").Instantiate();
		SetDragPreview(di2);
		Info = di2.GetPath();
		return Info;
	}
}
