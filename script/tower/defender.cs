using Godot;
using System;

public abstract partial class defender : Node2D
{
	protected int _cost = 0; 
    protected string _name;

	public abstract void Init();

	public int cost
	{
		get
		{
			return _cost;
		}
	}

}