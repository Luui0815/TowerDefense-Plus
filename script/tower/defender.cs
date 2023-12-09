using Godot;
using System;

public abstract partial class defender : Node2D
{
	[Signal]
    public delegate void generated_mine_moneyEventHandler(int _newmoney);//sollte man evtl. anders machen

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