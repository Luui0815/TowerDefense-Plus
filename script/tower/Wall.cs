using Godot;

public partial class Wall : Defender
{
    public Wall()
    {
        //TODO: (Maybe) Change values
        _delay = 200;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health=50;
    }

    public override void Action()
    {
        //Not needed until upgrades are implemented
    }

    public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.AnimationLooped += OnAnimationLooped;
        _animatedSprite.Play(_actionAnimation);
	}

    public override void _Process(double delta)
	{
        if(Health<=0)
        {
            OnDefenderDefeated();
        }
        else
        {
            _animatedSprite.Frame =5- Health / 10;
        }
    }

    private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "death")
            Destroy();
    }
}
