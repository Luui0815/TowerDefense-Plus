using Godot;
using TowerDefense;

public partial class MapLane : Node2D
{
	private int _laneNr;

	[Signal]
	public delegate void AllEnemiesDefeatedEventHandler(int laneNr);

	[Signal]
	public delegate void EnemyCrossedLaneEventHandler(int laneNr);

	public MapField[] _fields = new MapField[10];

	public void Init(int laneNr, FieldType[] fieldTypes)
	{
		_laneNr = laneNr;
		
		PackedScene fieldScene = GD.Load<PackedScene>("res://scene/map/MapField.tscn");
		Vector2 fieldPosition = new Vector2(0,0);
		for (int i = 0; i < 10; i++)
		{
			MapField field = (MapField)fieldScene.Instantiate();
			field.Init(fieldTypes[i], i + (10 * laneNr));
			field.Position = fieldPosition;
			AddChild(field);
			fieldPosition.X += 108;
			_fields[i] = field;
		}
	}
}
