using Godot;
using System;

public partial class MapLane : Node2D
{
	[Signal]
	public delegate void AllEnemiesDefeatedEventHandler();

	[Signal]
	public delegate void EnemyCrossedLaneEventHandler();
}
