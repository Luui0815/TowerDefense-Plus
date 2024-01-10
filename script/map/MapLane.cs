using Godot;
using System.Collections.Generic;
using TowerDefense;

public partial class MapLane : Node2D
{
	private int _laneNr;
	private Area2D _LawnMowerArea;
	private LawnMower _LawnMower;

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
		Vector2 fieldPosition = new Vector2(80, 0);
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
		_LawnMowerArea = GetNode<Area2D>("LawnMoverArea");
		_LawnMower =(LawnMower) GetNode<CharacterBody2D>("LawnMower");
	}

	private void _on_lawn_mover_area_area_entered(Area2D area)
    {
		if (area.GetParent() is Enemy && area.Name == "HitboxArea" && !_LawnMower.ActivateLawnMover)
		{
			GD.Print("Enemy hat die heilige Forte erreicht. Starte Gegenangriff!");
			_LawnMower.ActivateLawnMover=true;
		}
		else if(area.GetParent() is Enemy && area.Name == "HitboxArea")
        {
            EmitSignal(SignalName.EnemyCrossedLane, _laneNr);
            GD.Print("Enemy hat die heilige Forte zerstoert!");
        }
	}
}
