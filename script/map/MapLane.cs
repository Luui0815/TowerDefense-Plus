using Godot;
using System;
using TowerDefense;

public partial class MapLane : Node2D
{
	private int _levelNr;

	[Signal]
	public delegate void AllEnemiesDefeatedEventHandler(int laneNr);

	[Signal]
	public delegate void EnemyCrossedLaneEventHandler(int laneNr);

	public void Init(int levelNr, FieldType[] fieldTypes)
	{
		_levelNr = levelNr;
		//TODO: Implement field init
	}
}
