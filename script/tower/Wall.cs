public partial class Wall : Defender
{
    public Wall()
    {
        //TODO: (Maybe) Change values
        _delay = 200;
        _animationDelay = 1;
        _actionAnimation = "idle";
    }

    public override void Action()
    {
        //Not needed until upgrades are implemented
    }
}
