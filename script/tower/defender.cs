using Godot;

public abstract partial class Defender : GameEntity
{
    protected string _name;
	protected AnimatedSprite2D _sprite;

	public void Init(string towerName)
	{
		_name = towerName;
		_sprite = GetNode<AnimatedSprite2D>("AnimationSprite");
		_sprite.Play();
	}

	public int GetTowerCost()
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(_name);
        return towerSettings.Cost;
    }

}