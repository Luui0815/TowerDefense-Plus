using Godot;

public abstract partial class Defender : GameEntity
{
	[Signal]
    public delegate void generated_mine_moneyEventHandler(int _newmoney);//sollte man evtl. anders machen

	protected int _cost = 0; 
    protected string _name;

	public abstract void Init();

	public int Cost
	{
		get
		{
			return _cost;
		}
	}

	protected int GetTowerCost(string towerName)
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(towerName);
        return towerSettings.Cost;
    }

}