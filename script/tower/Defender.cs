using Godot;
using TowerDefense;

public abstract partial class Defender : GameEntity
{
	protected string _name;
    protected AnimatedSprite2D _animatedSprite;
	protected Area2D _HitboxArea;
    public bool DefenderDefeated = false;

    public void Init(string towerName)
	{
		_name = towerName;
	}

	public int GetTowerCost()
	{
		TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
		TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(_name);
		return towerSettings.Cost;
	}

    public override void Destroy()
    {
        MapField field = (MapField) GetParent();
		field.Tower = null;
		QueueFree();
    }
    protected void OnDefenderDefeated()
    {
        DefenderDefeated = true;
        if(_HitboxArea != null)
            _HitboxArea.QueueFree();
        _animatedSprite.Play("death");
    }

    public override void Action()
    {
    }
}