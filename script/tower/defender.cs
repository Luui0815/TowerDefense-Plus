using Godot;

public abstract partial class Defender : GameEntity
{
    protected string _name;

	public abstract void Init();

	public int GetTowerCost()
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(_name);
        return towerSettings.Cost;
    }

}