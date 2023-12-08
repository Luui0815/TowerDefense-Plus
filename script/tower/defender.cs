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

	protected int get_Tower_cost(string towerName)
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(towerName);
        return towerSettings.Cost;
    }

}