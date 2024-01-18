using Godot;
using System;
using TowerDefense;

public partial class TowerSelectionGridItem : Control
{
	private bool _animated;
	private Label _label;
	private AnimatedSprite2D _animatedSprite;
	private Button _button;
	private Area2D _area;
	private string _towerName;
	private bool _playAnimation;
    public Button TowerButton
    {
		get
		{
			return _button;
		}
    }
	
    public void Init(bool Animation,string TowerName)
	{
		_animated = Animation;
        _button = GetNode<Button>("Button");
		_button.Text = TowerName;
		_towerName = TowerName;
		_area = GetNode<Area2D>("Area2D");
		if(!Animation)
			_area.Visible= false;
    }
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		switch (_towerName)
		{
			case "knight":
				{
					_label.Text = "Ritter";
					break;
				}
			case "spearman":
				{
					_label.Text = "Speerwerferin";
					break;
				}
			case "wall":
					{
					_label.Text = "Mauer";
					break;
				}
            case "goldmine":
                {
                    _label.Text = "Goldmine";
                    break;
                }
            case "archer":
                {
                    _label.Text = "Bogenschütze";
                    break;
                }
            case "fire_trap":
                {
                    _label.Text = "Feuerfalle";
                    break;
                }
            case "caltrop_trap":
                {
                    _label.Text = "Bärenfalle";
                    break;
                }
        }
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.AnimationLooped += OnAnimationLooped;

        if (_animated)
			_animatedSprite.Play(_towerName + "_animation");
		else
            _animatedSprite.Play(_towerName);

    }
	public override void _Process(double delta)
	{
		if (_button.IsHovered()&&!_animated)
			_playAnimation = true;
		else
			_playAnimation = false;	
	}


	private void OnAnimationLooped()
	{
		if(!_animated)
		{
            if (_playAnimation)
                _animatedSprite.Play(_towerName + "_animation");
            else
                _animatedSprite.Play(_towerName);
        }
		else
		{
			if(_animatedSprite.Animation=="spearman_attack_range" || _animatedSprite.Animation=="archer_attack")
			{
                KnightEnemy enemy = new()
                {
                    Position = new(Position.X + 500, Position.Y)
                };
                TowerProjectile projectile = (TowerProjectile)GD.Load<PackedScene>("res://scene/tower/TowerProjectile.tscn").Instantiate();
				if(_animatedSprite.Animation == "spearman_attack_range")
				{
                    projectile.Init(enemy, 5f, ProjectileType.Spear, null);
                    projectile.Position = new Vector2(GlobalPosition.X, GlobalPosition.Y + 20);
                }
                else
				{
                    projectile.Init(enemy, 5f, ProjectileType.Arrow, null);
                    projectile.Position = new Vector2(GlobalPosition.X + 80, GlobalPosition.Y + 35);
                }
                AddChild(projectile);
            }
			
			Random random = new Random();

			if(_towerName == "spearman")
			{

				switch(random.Next(3))
				{
					case 0:
					{
						_animatedSprite.Play("spearman_animation");
						break;
					}
					case 1:
					{
						_animatedSprite.Play("spearman_attack_melee");
						break;
					}
					case 2:
					{
						_animatedSprite.Play("spearman_attack_range");
						break;
					}
				}
			}
			else if (_towerName == "goldmine" || _towerName == "wall" || _towerName == "caltrop_trap")
			{
                if (random.Next(20) == 0)
                    _animatedSprite.Play(_towerName + "_animation");
                else
                    _animatedSprite.Play(_towerName);
            }
			else if(_towerName == "fire_trap")
			{ }
			else
			{
				if (random.Next(2) == 0)
					_animatedSprite.Play(_towerName + "_animation");
				else
					_animatedSprite.Play(_towerName + "_attack");
			}
		}

    }

	private void _on_area_2d_area_exited(Area2D area)
	{
		area.GetParent().QueueFree();
	}
}
