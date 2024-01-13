using Godot;
using System;

public partial class tower_selection_grid : Control
{
	private bool _animated;
	private Label _label;
	private AnimatedSprite2D _animatedSprite;
	private Button _Button;
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
		if (_Button.IsHovered())
			_PlayAnimation = true;
		else
			_PlayAnimation = false;	
	}


	private void OnAnimationLooped()
	{
		if (_PlayAnimation && !_animated)
			_animatedSprite.Play(_TowerName + "_animation");
		else
            _animatedSprite.Play(_TowerName);
    }
}
