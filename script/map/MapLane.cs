using Godot;
using System.Collections.Generic;
using TowerDefense;

public partial class MapLane : Node2D
{
	private int _laneNr;
	private Area2D _lawnMowerArea;
	private LawnMower _lawnMower;

	[Signal]
	public delegate void AllEnemiesDefeatedEventHandler(int laneNr);

	[Signal]
    public delegate void EnemyCrossedLaneEventHandler(int laneNr);

	private MapField[] _fields = new MapField[10];

	public MapField[] Fields
	{
		get
		{
			return _fields;
		}
	}

	public void Init(int laneNr, FieldType[] fieldTypes)
	{
		_laneNr = laneNr;

		PackedScene fieldScene = GD.Load<PackedScene>("res://scene/map/MapField.tscn");
		Vector2 fieldPosition = new Vector2(95.5f, 0);
		for (int i = 0; i < 10; i++)
		{
			MapField field = (MapField)fieldScene.Instantiate();
			field.Init(fieldTypes[i], i + (10 * laneNr));
			field.Name = "MapField" + (i + (10 * laneNr));
			field.Position = fieldPosition;
			AddChild(field);
			fieldPosition.X += 100;
			_fields[i] = field;
		}
	}

	public override void _Ready()
	{
		_lawnMowerArea = GetNode<Area2D>("LawnMoverArea");
		_lawnMower =(LawnMower) GetNode<CharacterBody2D>("LawnMower");
	}

	private void _on_lawn_mover_area_area_entered(Area2D area)
    {
		if (area.GetParent() is Enemy && area.Name == "HitboxArea") {
			if (!IsInstanceValid(_lawnMower)) {
				EmitSignal(SignalName.EnemyCrossedLane, _laneNr);
			} else if (!_lawnMower.LawnMoverActivated){
				_lawnMower.LawnMoverActivated=true;
			}
		}
	}
}
