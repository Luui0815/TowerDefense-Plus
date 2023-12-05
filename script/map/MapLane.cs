using Godot;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using TowerDefense;

public partial class MapLane : Node2D
{ 
	private int _levelNr;
	private int _laneNr;

	[Signal]
	public delegate void AllEnemiesDefeatedEventHandler(int laneNr);

	[Signal]
	public delegate void EnemyCrossedLaneEventHandler(int laneNr);

	public void Init(int levelNr, FieldType[] fieldTypes, int LaneNr)
	{
		_levelNr = levelNr;
		_laneNr = LaneNr;

		Vector2 Fieldposition=new Vector2(0,0);
		
		for(int i = 0;i<10;i++)
		{
            MapField MF = (MapField)GD.Load<PackedScene>("res://scene/map/MapField.tscn").Instantiate();
			MF.Init(fieldTypes[i],i+(10*LaneNr));
			MF.Position = Fieldposition;
            AddChild(MF);
            Fieldposition.X += 108;
        }
	}
}
