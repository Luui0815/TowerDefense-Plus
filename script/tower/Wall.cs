public partial class Wall : Defender
{
    public Wall()
    {
        //TODO: (Maybe) Change values
        _delay = 200;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health=45;
    }

    public override void Action()
    {
        //Not needed until upgrades are implemented
    }

    	public override void _Ready()
	{
		_AttackArea = GetNode<Area2D>("AttackArea");
		_AttackTimer = GetNode<Timer>("AttackTimer");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		_AttackTimer.WaitTime= _delay; ;
		_animatedSprite.Play(_actionAnimation);
	}
}
