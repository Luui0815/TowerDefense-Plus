using Godot;
using Godot.Collections;
using System;
using TowerDefense;

public partial class tower_selection_grid : Control
{
	private bool _animated;
	private Label _label;
	private AnimatedSprite2D _animatedSprite;
	private Button _Button;
	private Area2D _area;
	private string _TowerName;
	private bool _PlayAnimation;
    public Button button
    {
		get
		{
			return _Button;
		}
    }
	//public string Name
	//{
	//	get
	//	{
	//		return _TowerName;
	//	}
	//}
    public void Init(bool Animation,string TowerName)
	{
		_animated = Animation;
        _Button = GetNode<Button>("Button");
		_Button.Text = TowerName;
		_TowerName = TowerName;
		_area = GetNode<Area2D>("Area2D");
		if(!Animation)
			_area.Visible= false;
    }
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		switch (_TowerName)
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
                    _label.Text = "Bogenschuetze";
                    break;
                }
            case "fire_trap":
                {
                    _label.Text = "Feuerfalle";
                    break;
                }
            case "caltrop_trap":
                {
                    _label.Text = "Baerenfalle";
                    break;
                }
        }
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.AnimationLooped += OnAnimationLooped;

        if (_animated)
			_animatedSprite.Play(_TowerName + "_animation");
		else
            _animatedSprite.Play(_TowerName);

    }
	public override void _Process(double delta)
	{
		if (_Button.IsHovered()&&!_animated)
			_PlayAnimation = true;
		else
			_PlayAnimation = false;	
	}


	private void OnAnimationLooped()
	{
		if(!_animated)
		{
            if (_PlayAnimation)
                _animatedSprite.Play(_TowerName + "_animation");
            else
                _animatedSprite.Play(_TowerName);
        }
		else
		{
			if(_animatedSprite.Animation=="spearman_attack_range" || _animatedSprite.Animation=="archer_attack")
			{
				KnightEnemy enemy= new();
				enemy.Position = new(Position.X+300,Position.Y);
                TowerProjectile projectile = (TowerProjectile)GD.Load<PackedScene>("res://scene/tower/TowerProjectile.tscn").Instantiate();
				if(_animatedSprite.Animation == "spearman_attack_range")
				{
                    projectile.Init(enemy, 5f, ProjectileType.Spear, null);
                    projectile.Position = new Vector2(GlobalPosition.X - 10, GlobalPosition.Y + 20);
                }
                else
				{
                    projectile.Init(enemy, 5f, ProjectileType.Arrow, null);
                    projectile.Position = new Vector2(GlobalPosition.X + 80, GlobalPosition.Y + 55);
                }
                AddChild(projectile);
            }

			if(_TowerName == "spearman")
			{
				switch(GD.Randi()%3)
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
			else if (_TowerName == "goldmine" || _TowerName == "wall" || _TowerName == "caltrop_trap")
			{
                if (GD.Randi() % 1000 == 0)
                    _animatedSprite.Play(_TowerName);
                else
                    _animatedSprite.Play(_TowerName + "_animation");
            }
			else if(_TowerName == "fire_trap")
			{ }
			else
			{
				if (GD.Randi() % 2 == 0)
					_animatedSprite.Play(_TowerName + "_animation");
				else
					_animatedSprite.Play(_TowerName + "_attack");
			}
		}

    }

	private void _on_area_2d_area_exited(Area2D area)
	{
		area.GetParent().QueueFree();
	}
}
